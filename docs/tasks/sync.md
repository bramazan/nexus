1. Endpoint Tanımı
• HTTP Method: POST
• URL: /api/v1/integrations/{integration_id}/sync
• Amaç: Kullanıcıdan (veya Cron Job'dan) gelen istek, veri çekme işleminin kapsamını belirlemelidir. bu veri setine uygun şekilde raw data çekilip async olarak db ye yazılır.

--------------------------------------------------------------------------------
2. Request (İstek) Yapısı
Kullanıcıdan (veya Cron Job'dan) gelen istek, veri çekme işleminin kapsamını belirlemelidir.
Örnek JSON Body:
{
  "start_date": "2024-01-01T00:00:00Z",  // Opsiyonel
  "end_date": "2024-01-07T23:59:59Z",    // Opsiyonel
  "entity_types": ["pull_request", "commit"], // Opsiyonel (Boşsa hepsini çeker)
  "force_restart": false // Opsiyonel (Önceki iş takıldıysa zorla başlatmak için)
}
Parametrelerin İşlevi:
• integration_id (Path Param): Hangi araçla konuşuyoruz? (Örn: GitLab - Backend Repo).
• start_date / end_date:
    ◦ Eğer dolu gelirse: "Backfill" (Tarihçe doldurma) veya "Repair" (Hatalı dönemi düzeltme) modunda çalışır.
    ◦ Eğer boş gelirse (Önerilen): Sistem integrations tablosundaki last_sync_at alanına bakar ve "Artımlı Senkronizasyon" (Incremental Sync) başlatır.
• entity_types: Bazen sadece PR'ları güncellemek istersiniz (çünkü commitler çok fazladır ve değişmez). Bu filtre performansı artırır.

--------------------------------------------------------------------------------
3. Response (Yanıt) Yapısı
Bu endpoint işlemi o an yapmayacağı için, HTTP 200 OK yerine HTTP 202 Accepted dönmek en doğru standarttır. Bu, "İsteğini aldım, sıraya koydum, ama henüz bitmedi" demektir.
Başarılı Yanıt (HTTP 202 Accepted):
{
  "job_id": "550e8400-e29b-41d4-a716-446655440000",
  "status": "PENDING",
  "message": "GitLab senkronizasyon işi kuyruğa alındı.",
  "queue_position": 1,
  "estimated_start": "Immediately"
}
Hata Yanıtı (HTTP 409 Conflict): Eğer aynı entegrasyon için şu an zaten çalışan (IN_PROGRESS) bir iş varsa, yenisini oluşturmamak gerekir.
{
  "error": "JobAlreadyRunning",
  "message": "Bu entegrasyon için şu anda aktif bir senkronizasyon işlemi var.",
  "existing_job_id": "123e4567-e89b-..."
}