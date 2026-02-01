-- Clean up existing data (Optional: Use carefully)
-- TRUNCATE TABLE tool_accounts, team_members, service_repositories, services, repositories, teams, users, integrations, workspaces, metric_thresholds CASCADE;

-- 1. Workspaces
INSERT INTO workspaces (id, name, created, created_by) VALUES ('a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Ödeal Teknoloji', NOW(), 'system') ON CONFLICT DO NOTHING;

-- 2. Integrations
INSERT INTO integrations (id, workspace_id, type, name, config, created, created_by) VALUES ('b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'jira', 'Main Jira Instance', '{"baseUrl": "https://odeal.atlassian.net", "email": "burak@ode.al", "apiToken": "YOUR_ATLASSIAN_API_TOKEN"}', NOW(), 'system') ON CONFLICT DO NOTHING;
INSERT INTO integrations (id, workspace_id, type, name, config, created, created_by) VALUES ('c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'gitlab', 'Corporate GitLab', '{"baseUrl": "https://git.odeal.com", "accessToken": "Ai-qzOFf4-yhu5OWFIbQZm86MQp1OjFlCA.01.0y1laqe1t", "groupId": "3"}', NOW(), 'system') ON CONFLICT DO NOTHING;

-- 3. Users
INSERT INTO users (id, full_name, email, created, created_by) VALUES
('96475646-0ddf-50dc-ab5b-dc498c2504db', 'Adem Taçyıldız', 'adem.tacyildiz@ode.al', NOW(), 'system'),
('888d7286-7dba-5f62-826c-c4082ce66afa', 'Ahmet Sağlam', 'ahmet.saglam@ode.al', NOW(), 'system'),
('f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'Alican İnan', 'alican.inan@ode.al', NOW(), 'system'),
('09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'Anıl Akkaya', 'anil.akkaya@ode.al', NOW(), 'system'),
('3fcd0f39-8866-5407-a193-bb81ceab588e', 'Anıl Sakaryalı', 'anil.sakaryali@ode.al', NOW(), 'system'),
('411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'Burak Ramazan', 'burak@ode.al', NOW(), 'system'),
('551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'Elif Burçak Namver', 'burcak.namver@ode.al', NOW(), 'system'),
('590323a2-2dfc-579c-be0d-36791fbaed97', 'Erdem Öden', 'erdem.oden@ode.al', NOW(), 'system'),
('880790c3-c4d8-53c0-beab-289f96a57053', 'Gökhan İbrikçi', 'gokhan.ibrikci@ode.al', NOW(), 'system'),
('905e5226-83b8-5e1e-8053-a0fc570740e1', 'Hacı Burak Tahmaz', 'burak.tahmaz@ode.al', NOW(), 'system'),
('6f0945ad-4bf5-58bf-8847-08f390ba7648', 'Hüseyin Mutlu', 'huseyin.mutlu@ode.al', NOW(), 'system'),
('6eb987e8-d870-5604-bd5b-67a30d13b903', 'Mehmet Yetiş', 'mehmet.yetis@ode.al', NOW(), 'system'),
('5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'Mert Kaim', 'mert.kaim@ode.al', NOW(), 'system'),
('62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'Ahmet Umur Gültekin', 'umur.gultekin@ode.al', NOW(), 'system'),
('8d113595-682d-5d60-9970-74b5087279ad', 'Metin İsfendiyar', 'metin.isfendiyar@ode.al', NOW(), 'system'),
('8af193e1-1748-5b5f-9218-57593c0fd7e3', 'Mustafa Çolakoğlu', 'mustafa.colakoglu@ode.al', NOW(), 'system'),
('51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'Resul Bozdemir', 'resul.bozdemir@ode.al', NOW(), 'system'),
('9bb0636e-614a-5915-8d67-02dba568f12d', 'Volkan Kurt', 'volkan.kurt@ode.al', NOW(), 'system'),
('73b5e99d-61bd-57b7-840b-0cece05fadb1', 'Yakup Doğan', 'yakup.dogan@ode.al', NOW(), 'system'),
('c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'Yasir Arslan', 'yasir.arslan@ode.al', NOW(), 'system'),
('659bd5b5-df2f-5aca-8977-2738d49196d1', 'Bilal Cihangir', 'bilal.cihangir@ode.al', NOW(), 'system'),
('588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'Tahsin Civelek', 'tahsin.civelek@ode.al', NOW(), 'system'),
('eed31354-2423-525b-8ed5-d4c5c3e58b78', 'Mustafa Hasan', 'mustafa.hasan@ode.al', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- 4. Teams
INSERT INTO teams (id, workspace_id, name, parent_team_id, created, created_by) VALUES ('51103da8-eb62-5383-935c-95c5f52a55a7', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Common', NULL, NOW(), 'system') ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created, created_by) VALUES ('edb15541-d186-59eb-8876-d067098d2371', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW(), 'system') ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created, created_by) VALUES ('81be54c0-c2e5-5790-8978-619825aa0263', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Finance', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW(), 'system') ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created, created_by) VALUES ('ec81d79e-b13a-548e-8082-3c479facae8a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Service Banking', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW(), 'system') ON CONFLICT DO NOTHING;

-- 5. Team Members
INSERT INTO team_members (id, team_id, user_id, role, created, created_by) VALUES
('2490176b-d226-458f-b5a9-0162bb608dc8', '81be54c0-c2e5-5790-8978-619825aa0263', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'Associate Software Engineer', NOW(), 'system'),
('bf492bba-7550-4842-b03a-0a869175b592', 'edb15541-d186-59eb-8876-d067098d2371', '888d7286-7dba-5f62-826c-c4082ce66afa', 'QA Engineer', NOW(), 'system'),
('dfebaf3e-3e6f-4130-a0e0-d2ef8792e356', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'Senior Software Engineer', NOW(), 'system'),
('c3490232-2897-4be8-9c46-f64d6e5a67a4', 'edb15541-d186-59eb-8876-d067098d2371', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'QA Engineer', NOW(), 'system'),
('400b0b5a-cdf0-434f-bff7-77e9c0a4b89c', 'edb15541-d186-59eb-8876-d067098d2371', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'Software Engineer', NOW(), 'system'),

('312b3fef-8a92-4197-9622-45c13dcda722', '81be54c0-c2e5-5790-8978-619825aa0263', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'Senior Software Engineer', NOW(), 'system'),
('bf2830a2-0bef-41a0-a5e1-71890d7d85a3', '81be54c0-c2e5-5790-8978-619825aa0263', '590323a2-2dfc-579c-be0d-36791fbaed97', 'Software Engineer', NOW(), 'system'),
('0455ae91-a3a9-497f-91d0-de7fff758aa1', 'ec81d79e-b13a-548e-8082-3c479facae8a', '880790c3-c4d8-53c0-beab-289f96a57053', 'Software Development Team Lead', NOW(), 'system'),
('b18b377d-306f-4749-bbf7-6bfb0fa25ae6', 'ec81d79e-b13a-548e-8082-3c479facae8a', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'QA Engineer', NOW(), 'system'),
('d6b9b76a-3d71-450e-bdc8-6a509017d94c', '81be54c0-c2e5-5790-8978-619825aa0263', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'Associate Software Engineer', NOW(), 'system'),
('686b4350-12f5-4cc6-a0b1-f35569d62d70', 'edb15541-d186-59eb-8876-d067098d2371', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'QA Engineer', NOW(), 'system'),
('815eb396-735c-48d3-ab92-7e207d0345a0', 'ec81d79e-b13a-548e-8082-3c479facae8a', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'Software Engineer', NOW(), 'system'),
('fea73946-b3a6-4d7a-8a9a-bf0c7ccdd458', 'ec81d79e-b13a-548e-8082-3c479facae8a', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'Product Owner', NOW(), 'system'),
('17f12c0f-592a-4324-838e-ac1edb854d08', 'edb15541-d186-59eb-8876-d067098d2371', '8d113595-682d-5d60-9970-74b5087279ad', 'Technical Analyst', NOW(), 'system'),
('2a6c2639-e9ea-4330-b93c-73043e41d459', 'edb15541-d186-59eb-8876-d067098d2371', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'Software Engineer', NOW(), 'system'),
('6d448645-8eda-413d-a9a0-898272b31361', 'edb15541-d186-59eb-8876-d067098d2371', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'Software Engineer', NOW(), 'system'),
('babc703f-9b77-49ca-abc2-0823b7aeadb3', 'edb15541-d186-59eb-8876-d067098d2371', '9bb0636e-614a-5915-8d67-02dba568f12d', 'Software Architect', NOW(), 'system'),
('e4906dd1-34a0-4df5-8c9a-8deb599c9686', 'edb15541-d186-59eb-8876-d067098d2371', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'QA Developer', NOW(), 'system'),
('8c81e674-fc91-4888-9f30-4048a5a47891', 'edb15541-d186-59eb-8876-d067098d2371', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'Stajyer', NOW(), 'system'),
('8182af1b-c750-4612-9ca8-62bf41264eec', 'edb15541-d186-59eb-8876-d067098d2371', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'Product Owner', NOW(), 'system'),
('d4515029-c0e4-43ae-931c-3b807178af3a', 'edb15541-d186-59eb-8876-d067098d2371', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'Product Owner', NOW(), 'system'),
('f6665932-2d09-40a0-a513-31879348cded', '81be54c0-c2e5-5790-8978-619825aa0263', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'Product Owner', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- 6. Tool Accounts (GitLab username & Jira email)
INSERT INTO tool_accounts (id, user_id, integration_id, external_id, external_username, created, created_by) VALUES
('853f7b7f-76eb-4529-97cb-1b174c532b05', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'adem.tacyildiz', 'adem.tacyildiz', NOW(), 'system'),
('13d7a3da-211d-433a-b7e9-4d1819bdf1e4', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'adem.tacyildiz@ode.al', 'adem.tacyildiz@ode.al', NOW(), 'system'),
('7a6c234d-ec2c-40d1-8f26-9568d7cd8361', '888d7286-7dba-5f62-826c-c4082ce66afa', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'ahmet.saglam', 'ahmet.saglam', NOW(), 'system'),
('b252377f-7771-4074-a424-e97543076eba', '888d7286-7dba-5f62-826c-c4082ce66afa', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'ahmet.saglam@ode.al', 'ahmet.saglam@ode.al', NOW(), 'system'),
('3f30fb7e-c188-489b-988a-fd648c2d2744', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'alican.inan', 'alican.inan', NOW(), 'system'),
('eeb7989f-a95d-45ce-87e3-c142a0d79a2d', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'alican.inan@ode.al', 'alican.inan@ode.al', NOW(), 'system'),
('223ac40e-de95-4724-b278-193fe358ef69', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'anil.akkaya', 'anil.akkaya', NOW(), 'system'),
('73d5d49f-cfd9-4175-be9a-055864156606', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'anil.akkaya@ode.al', 'anil.akkaya@ode.al', NOW(), 'system'),
('d3143f12-f913-481e-b9a7-3b5caa890e7f', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'anil.sakaryali', 'anil.sakaryali', NOW(), 'system'),
('ad8b8a9f-0bde-430d-89f7-b0e171c285b3', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'anil.sakaryali@ode.al', 'anil.sakaryali@ode.al', NOW(), 'system'),
('a4d65fc6-d806-49ff-b86d-a756687bef0a', '411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burak', 'burak', NOW(), 'system'),
('f97a58e7-448e-44f9-a512-61d9c33cf2d3', '411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burak@ode.al', 'burak@ode.al', NOW(), 'system'),
('152d2cf8-c4ef-4c29-be46-bc201539e2a2', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burcak.namver', 'burcak.namver', NOW(), 'system'),
('eaaf9996-8c87-4d94-8a19-eab6b40a65af', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burcak.namver@ode.al', 'burcak.namver@ode.al', NOW(), 'system'),
('cd80d5a4-ebe6-4bd2-a79e-7a3bd9a45968', '590323a2-2dfc-579c-be0d-36791fbaed97', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'erdem.oden', 'erdem.oden', NOW(), 'system'),
('9595e784-7354-489b-8d0c-bef55026a6d8', '590323a2-2dfc-579c-be0d-36791fbaed97', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'erdem.oden@ode.al', 'erdem.oden@ode.al', NOW(), 'system'),
('02159c45-ebd8-4a78-ba77-c7b4221dc8b8', '880790c3-c4d8-53c0-beab-289f96a57053', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'gokhan.ibrikci', 'gokhan.ibrikci', NOW(), 'system'),
('83cba3d9-790f-4aa8-80c8-36cb40a8c74f', '880790c3-c4d8-53c0-beab-289f96a57053', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'gokhan.ibrikci@ode.al', 'gokhan.ibrikci@ode.al', NOW(), 'system'),
('39b88c63-984e-4dda-b09b-d09cedc0bcad', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burak.tahmaz', 'burak.tahmaz', NOW(), 'system'),
('a806236e-ec36-417d-adab-9f6941065cd7', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burak.tahmaz@ode.al', 'burak.tahmaz@ode.al', NOW(), 'system'),
('558f6086-adb7-4772-9830-8ab8777e3ab4', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'huseyin.mutlu', 'huseyin.mutlu', NOW(), 'system'),
('3ab33345-cd17-4d59-b918-3671958387ab', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'huseyin.mutlu@ode.al', 'huseyin.mutlu@ode.al', NOW(), 'system'),
('c8cbf118-75c0-4591-99c4-d915eda1c595', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mehmet.yetis', 'mehmet.yetis', NOW(), 'system'),
('256446cc-9610-415c-94ce-d6af65c97136', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mehmet.yetis@ode.al', 'mehmet.yetis@ode.al', NOW(), 'system'),
('a21ccf24-dcf8-4ac2-841d-757748c27a87', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mert.kaim', 'mert.kaim', NOW(), 'system'),
('8c623d26-10cc-4e0f-b4b3-e67f1bf8abe6', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mert.kaim@ode.al', 'mert.kaim@ode.al', NOW(), 'system'),
('732d9d98-0e84-4219-8947-b1d7534a1c75', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'umur.gultekin', 'umur.gultekin', NOW(), 'system'),
('bf970b2d-0959-4392-b58e-2272471d30a6', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'umur.gultekin@ode.al', 'umur.gultekin@ode.al', NOW(), 'system'),
('21f82150-94b4-4535-8d87-283eefa782f8', '8d113595-682d-5d60-9970-74b5087279ad', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'metin.isfendiyar', 'metin.isfendiyar', NOW(), 'system'),
('95ff60f9-ac45-414e-9d9d-6b507b4c2fae', '8d113595-682d-5d60-9970-74b5087279ad', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'metin.isfendiyar@ode.al', 'metin.isfendiyar@ode.al', NOW(), 'system'),
('6f731f1b-fbed-412e-9960-4cbbe0f28ff1', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mustafa.colakoglu', 'mustafa.colakoglu', NOW(), 'system'),
('8188ed47-22ce-4cc9-9036-ba92b81b5ec5', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mustafa.colakoglu@ode.al', 'mustafa.colakoglu@ode.al', NOW(), 'system'),
('43815d6e-55dd-4ffd-b83e-ecb9711f01b7', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'resul.bozdemir', 'resul.bozdemir', NOW(), 'system'),
('1fa2f69d-e9ee-4e33-b5d6-86ac5a5a9139', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'resul.bozdemir@ode.al', 'resul.bozdemir@ode.al', NOW(), 'system'),
('833382d1-f500-4649-81e7-f905674d3705', '9bb0636e-614a-5915-8d67-02dba568f12d', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'volkan.kurt', 'volkan.kurt', NOW(), 'system'),
('7c4ff69a-d018-405d-962d-d647db89258d', '9bb0636e-614a-5915-8d67-02dba568f12d', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'volkan.kurt@ode.al', 'volkan.kurt@ode.al', NOW(), 'system'),
('08be156b-73a3-4272-8b34-3c72e73eb917', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'yakup.dogan', 'yakup.dogan', NOW(), 'system'),
('052e7278-989b-4548-a9bb-db7623a0344c', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'yakup.dogan@ode.al', 'yakup.dogan@ode.al', NOW(), 'system'),
('713526c4-ddff-4f5f-8917-584fdd217089', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'yasir.arslan', 'yasir.arslan', NOW(), 'system'),
('6de495f0-b90d-4f40-ade8-514d7face96e', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'yasir.arslan@ode.al', 'yasir.arslan@ode.al', NOW(), 'system'),
('6a9b2ec9-aa86-43c1-94be-8b8790524ed2', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bilal.cihangir', 'bilal.cihangir', NOW(), 'system'),
('e505e2cd-5afc-4055-af72-e79aaa218e98', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'bilal.cihangir@ode.al', 'bilal.cihangir@ode.al', NOW(), 'system'),
('9a4dd7da-89b6-4a41-a24d-12a17dcf72b0', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'tahsin.civelek', 'tahsin.civelek', NOW(), 'system'),
('a479bb72-2ad6-429b-af1d-84c310c75a56', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'tahsin.civelek@ode.al', 'tahsin.civelek@ode.al', NOW(), 'system'),
('5eaec70c-dae1-4fb5-bdac-15a9a4fd66f6', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mustafa.hasan', 'mustafa.hasan', NOW(), 'system'),
('cca981ed-636c-49f6-bf48-d5831512a7b3', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mustafa.hasan@ode.al', 'mustafa.hasan@ode.al', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- 7. Repositories
-- Finance
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, path_with_namespace, http_url_to_repo, is_active, created, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'finance-client-api-gateway', '514', 'https://git.odeal.com/odeal/finance/finance-client-api-gateway', 'main', 'odeal/finance/finance-client-api-gateway', 'https://git.odeal.com/odeal/finance/finance-client-api-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Transfer', '512', 'https://git.odeal.com/odeal/finance/transfer', 'main', 'odeal/finance/transfer', 'https://git.odeal.com/odeal/finance/transfer.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'finance-matching-app', '468', 'https://git.odeal.com/odeal/finance/finance-matching-app', 'main', 'odeal/finance/finance-matching-app', 'https://git.odeal.com/odeal/finance/finance-matching-app.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Accounting', '467', 'https://git.odeal.com/odeal/finance/Accounting', 'main', 'odeal/finance/Accounting', 'https://git.odeal.com/odeal/finance/Accounting.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Report', '466', 'https://git.odeal.com/odeal/finance/report', 'main', 'odeal/finance/report', 'https://git.odeal.com/odeal/finance/report.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'the', '450', 'https://git.odeal.com/odeal/finance/the', 'main', 'odeal/finance/the', 'https://git.odeal.com/odeal/finance/the.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payfin-scheduler', '449', 'https://git.odeal.com/odeal/finance/payfin-scheduler', 'main', 'odeal/finance/payfin-scheduler', 'https://git.odeal.com/odeal/finance/payfin-scheduler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payout-source', '442', 'https://git.odeal.com/odeal/finance/payout-source', 'main', 'odeal/finance/payout-source', 'https://git.odeal.com/odeal/finance/payout-source.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-payment-matcher', '440', 'https://git.odeal.com/odeal/finance/odeal-payment-matcher', 'main', 'odeal/finance/odeal-payment-matcher', 'https://git.odeal.com/odeal/finance/odeal-payment-matcher.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'history', '424', 'https://git.odeal.com/odeal/finance/history', 'main', 'odeal/finance/history', 'https://git.odeal.com/odeal/finance/history.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'tsm-handler', '423', 'https://git.odeal.com/odeal/finance/tsm-handler', 'main', 'odeal/finance/tsm-handler', 'https://git.odeal.com/odeal/finance/tsm-handler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-bank-report-handler', '422', 'https://git.odeal.com/odeal/finance/odeal-bank-report-handler', 'main', 'odeal/finance/odeal-bank-report-handler', 'https://git.odeal.com/odeal/finance/odeal-bank-report-handler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payback-callback-consumer', '382', 'https://git.odeal.com/odeal/finance/payback-callback-consumer', 'main', 'odeal/finance/payback-callback-consumer', 'https://git.odeal.com/odeal/finance/payback-callback-consumer.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payout', '67', 'https://git.odeal.com/odeal/finance/payout', 'main', 'odeal/finance/payout', 'https://git.odeal.com/odeal/finance/payout.git', true, NOW(), NULL)
ON CONFLICT DO NOTHING;

-- Payment
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, path_with_namespace, http_url_to_repo, is_active, created, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Docs', '619', 'https://git.odeal.com/odeal/payment/docs', 'main', 'odeal/payment/docs', 'https://git.odeal.com/odeal/payment/docs.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'recurring', '455', 'https://git.odeal.com/odeal/payment/recurring', 'main', 'odeal/payment/recurring', 'https://git.odeal.com/odeal/payment/recurring.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment-gateway', '444', 'https://git.odeal.com/odeal/payment/payment-gateway', 'main', 'odeal/payment/payment-gateway', 'https://git.odeal.com/odeal/payment/payment-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bkm', '432', 'https://git.odeal.com/odeal/payment/bkm', 'master', 'odeal/payment/bkm', 'https://git.odeal.com/odeal/payment/bkm.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'belbim', '431', 'https://git.odeal.com/odeal/payment/belbim', 'main', 'odeal/payment/belbim', 'https://git.odeal.com/odeal/payment/belbim.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment', '430', 'https://git.odeal.com/odeal/payment/payment', 'main', 'odeal/payment/payment', 'https://git.odeal.com/odeal/payment/payment.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'vpos-payment-gateway', '425', 'https://git.odeal.com/odeal/payment/vpos-payment-gateway', 'main', 'odeal/payment/vpos-payment-gateway', 'https://git.odeal.com/odeal/payment/vpos-payment-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mobile', '417', 'https://git.odeal.com/odeal/payment/mobile', 'main', 'odeal/payment/mobile', 'https://git.odeal.com/odeal/payment/mobile.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'pos-gateway', '332', 'https://git.odeal.com/odeal/payment/pos-gateway', 'main', 'odeal/payment/pos-gateway', 'https://git.odeal.com/odeal/payment/pos-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Payment Integration', '317', 'https://git.odeal.com/odeal/payment/payment-integration', 'main', 'odeal/payment/payment-integration', 'https://git.odeal.com/odeal/payment/payment-integration.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-consumers', '213', 'https://git.odeal.com/odeal/payment/odeal-consumers', 'master', 'odeal/payment/odeal-consumers', 'https://git.odeal.com/odeal/payment/odeal-consumers.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential-service', '118', 'https://git.odeal.com/odeal/payment/credential-service', 'master', 'odeal/payment/credential-service', 'https://git.odeal.com/odeal/payment/credential-service.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-fraud-old', '56', 'https://git.odeal.com/odeal/payment/odeal-fraud-old', 'master', 'odeal/payment/odeal-fraud-old', 'https://git.odeal.com/odeal/payment/odeal-fraud-old.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mobile-gateway', '44', 'https://git.odeal.com/odeal/payment/mobile-gateway', 'master', 'odeal/payment/mobile-gateway', 'https://git.odeal.com/odeal/payment/mobile-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Basket Test App', '19', 'https://git.odeal.com/odeal/payment/basket-test-app', 'main', 'odeal/payment/basket-test-app', 'https://git.odeal.com/odeal/payment/basket-test-app.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payfront-test-project', '4', 'https://git.odeal.com/odeal/payment/payfront-test-project', 'main', 'odeal/payment/payfront-test-project', 'https://git.odeal.com/odeal/payment/payfront-test-project.git', true, NOW(), NULL)
ON CONFLICT DO NOTHING;

-- Service Banking
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, path_with_namespace, http_url_to_repo, is_active, created, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'card-manager-v2', '639', 'https://git.odeal.com/odeal/service-banking/card-manager-v2', 'main', 'odeal/service-banking/card-manager-v2', 'https://git.odeal.com/odeal/service-banking/card-manager-v2.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'SSO', '628', 'https://git.odeal.com/odeal/service-banking/sso-odeal', 'main', 'odeal/service-banking/sso-odeal', 'https://git.odeal.com/odeal/service-banking/sso-odeal.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'identity', '615', 'https://git.odeal.com/odeal/service-banking/identity', 'main', 'odeal/service-banking/identity', 'https://git.odeal.com/odeal/service-banking/identity.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bank-integration', '568', 'https://git.odeal.com/odeal/service-banking/fiba-integration', 'main', 'odeal/service-banking/fiba-integration', 'https://git.odeal.com/odeal/service-banking/fiba-integration.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-gateway', '566', 'https://git.odeal.com/odeal/service-banking/odeal-gateway', 'main', 'odeal/service-banking/odeal-gateway', 'https://git.odeal.com/odeal/service-banking/odeal-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credit-manager', '551', 'https://git.odeal.com/odeal/service-banking/credit-manager', 'main', 'odeal/service-banking/credit-manager', 'https://git.odeal.com/odeal/service-banking/credit-manager.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'portal-manager', '545', 'https://git.odeal.com/odeal/service-banking/portal-manager', 'main', 'odeal/service-banking/portal-manager', 'https://git.odeal.com/odeal/service-banking/portal-manager.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'card-manager', '436', 'https://git.odeal.com/odeal/service-banking/card-manager', 'main', 'odeal/service-banking/card-manager', 'https://git.odeal.com/odeal/service-banking/card-manager.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'fiba-gateway', '428', 'https://git.odeal.com/odeal/service-banking/fiba-gateway', 'main', 'odeal/service-banking/fiba-gateway', 'https://git.odeal.com/odeal/service-banking/fiba-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'service-banking-scheduler', '420', 'https://git.odeal.com/odeal/service-banking/service-banking-scheduler', 'main', 'odeal/service-banking/service-banking-scheduler', 'https://git.odeal.com/odeal/service-banking/service-banking-scheduler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential-service', '404', 'https://git.odeal.com/odeal/service-banking/credential-service', 'main', 'odeal/service-banking/credential-service', 'https://git.odeal.com/odeal/service-banking/credential-service.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'member', '402', 'https://git.odeal.com/odeal/service-banking/member', 'main', 'odeal/service-banking/member', 'https://git.odeal.com/odeal/service-banking/member.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'otp', '383', 'https://git.odeal.com/odeal/service-banking/otp', 'main', 'odeal/service-banking/otp', 'https://git.odeal.com/odeal/service-banking/otp.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential', '363', 'https://git.odeal.com/odeal/service-banking/credential', 'main', 'odeal/service-banking/credential', 'https://git.odeal.com/odeal/service-banking/credential.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'portal-backend', '333', 'https://git.odeal.com/odeal/service-banking/portal-backend', 'main', 'odeal/service-banking/portal-backend', 'https://git.odeal.com/odeal/service-banking/portal-backend.git', true, NOW(), NULL)
ON CONFLICT DO NOTHING;

-- Common 
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, path_with_namespace, http_url_to_repo, is_active, created, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'client-api-gateway', '465', 'https://git.odeal.com/odeal/common/client-api-gateway', 'main', 'odeal/common/client-api-gateway', 'https://git.odeal.com/odeal/common/client-api-gateway.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'scheduler', '456', 'https://git.odeal.com/odeal/common/scheduler', 'main', 'odeal/common/scheduler', 'https://git.odeal.com/odeal/common/scheduler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'rest', '454', 'https://git.odeal.com/odeal/common/rest', 'main', 'odeal/common/rest', 'https://git.odeal.com/odeal/common/rest.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'okc-service', '448', 'https://git.odeal.com/odeal/common/okc-service', 'main', 'odeal/common/okc-service', 'https://git.odeal.com/odeal/common/okc-service.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment-handler', '445', 'https://git.odeal.com/odeal/common/payment-handler', 'main', 'odeal/common/payment-handler', 'https://git.odeal.com/odeal/common/payment-handler.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'okc', '443', 'https://git.odeal.com/odeal/common/okc', 'main', 'odeal/common/okc', 'https://git.odeal.com/odeal/common/okc.git', true, NOW(), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'aggregator', '433', 'https://git.odeal.com/odeal/common/aggregator', 'main', 'odeal/common/aggregator', 'https://git.odeal.com/odeal/common/aggregator.git', true, NOW(), NULL)
ON CONFLICT DO NOTHING;

-- 8. Services

-- Finance
INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('c4ed2a4b-5d35-4d28-8f2c-0d2c8e43c9e1', '81be54c0-c2e5-5790-8978-619825aa0263', 'Accounting API', 'Tier-1', 'Finance service', NOW(), 'system'),
('c3c2e3bd-3c4c-4d5b-8f87-2b69e1f56c9a', '81be54c0-c2e5-5790-8978-619825aa0263', 'Accounting Consumer Transaction Summary Report', 'Tier-1', 'Finance service', NOW(), 'system'),
('b1d8b7a2-8a2b-4b7a-9b2d-1a9f2e0b3d4c', '81be54c0-c2e5-5790-8978-619825aa0263', 'Bank Report Handler', 'Tier-1', 'Finance service', NOW(), 'system'),
('f2c4c6a1-1a5e-4c0b-9e5d-7f6d8b9a0c12', '81be54c0-c2e5-5790-8978-619825aa0263', 'History', 'Tier-1', 'Finance service', NOW(), 'system'),
('d7a3f0e4-6b7c-4c2b-8a1d-3e5f6a7b8c9d', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payback Callback Consumer', 'Tier-1', 'Finance service', NOW(), 'system'),
('a8c9d0e1-2f3b-4a5c-9d6e-7b8c9a0d1e2f', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payback Consumer', 'Tier-1', 'Finance service', NOW(), 'system'),
('e1f2a3b4-5c6d-4e7f-8a9b-0c1d2e3f4a5b', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payback V1', 'Tier-1', 'Finance service', NOW(), 'system'),
('b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payfin Scheduler', 'Tier-1', 'Finance service', NOW(), 'system'),
('c5d6e7f8-9a0b-4c1d-8e2f-3a4b5c6d7e8f', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payment Matcher', 'Tier-1', 'Finance service', NOW(), 'system'),
('a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payout API', 'Tier-1', 'Finance service', NOW(), 'system'),
('f0e1d2c3-b4a5-4c6d-8e7f-9a0b1c2d3e4f', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payout Consumer', 'Tier-1', 'Finance service', NOW(), 'system'),
('d1e2f3a4-b5c6-4d7e-8f9a-0b1c2d3e4f5a', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payout Source API', 'Tier-1', 'Finance service', NOW(), 'system'),
('e2f3a4b5-c6d7-4e8f-9a0b-1c2d3e4f5a6b', '81be54c0-c2e5-5790-8978-619825aa0263', 'Payout Source Handler', 'Tier-1', 'Finance service', NOW(), 'system'),
('f3a4b5c6-d7e8-4f9a-0b1c-2d3e4f5a6b7c', '81be54c0-c2e5-5790-8978-619825aa0263', 'Report', 'Tier-1', 'Finance service', NOW(), 'system'),
('a4b5c6d7-e8f9-4a0b-1c2d-3e4f5a6b7c8d', '81be54c0-c2e5-5790-8978-619825aa0263', 'THE V3', 'Tier-1', 'Finance service', NOW(), 'system'),
('b5c6d7e8-f9a0-4b1c-2d3e-4f5a6b7c8d9e', '81be54c0-c2e5-5790-8978-619825aa0263', 'TSM Handler', 'Tier-1', 'Finance service', NOW(), 'system')
ON CONFLICT DO NOTHING;

INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('8f1f2a3b-4c5d-4e6f-8a9b-0c1d2e3f4a5b', '81be54c0-c2e5-5790-8978-619825aa0263', 'finance-client-api-gateway', 'Tier-1', 'Finance service', NOW(), 'system'),
('9a2b3c4d-5e6f-4a7b-8c9d-1e2f3a4b5c6d', '81be54c0-c2e5-5790-8978-619825aa0263', 'transfer-api', 'Tier-1', 'Finance service', NOW(), 'system'),
('0b3c4d5e-6f7a-4b8c-9d0e-2f3a4b5c6d7e', '81be54c0-c2e5-5790-8978-619825aa0263', 'transfer-consumer', 'Tier-1', 'Finance service', NOW(), 'system'),
('1c4d5e6f-7a8b-4c9d-8e0f-3a4b5c6d7e8f', '81be54c0-c2e5-5790-8978-619825aa0263', 'transfer-scheduler', 'Tier-1', 'Finance service', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- Service Banking
INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('1f9a7c3d-2a1b-4c8e-9b7c-3f5a6b7c8d9e', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Card Manager API', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('2a8b6c4d-3e2f-4a9b-8c7d-4e5f6a7b8c9d', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Card Manager Consumer', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('3b7c5d4e-4f3a-4b8c-9d6e-5f6a7b8c9d0e', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Credit Manager API', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('4c6d5e7f-5a4b-4c9d-8e7f-6a7b8c9d0e1f', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Fiba Gateway', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('5d7e6f8a-6b5c-4d0e-9f8a-7b8c9d0e1f2a', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Fiba Integration', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('6e8f7a9b-7c6d-4e1f-8a9b-8c9d0e1f2a3b', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Odeal Gateway', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('7f9a8b0c-8d7e-4f2a-9b0c-9d0e1f2a3b4c', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Portal Backend', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('8a0b9c1d-9e8f-4a3b-8c1d-0e1f2a3b4c5d', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Portal Manager', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('9b1c0d2e-0f9a-4b4c-9d2e-1f2a3b4c5d6e', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Credit Manager Consumer', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('a0c1d2e3-1a0b-4c5d-8e3f-2a3b4c5d6e7f', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Credit Manager Producer', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('b1d2e3f4-2b1c-4d6e-9f4a-3b4c5d6e7f8a', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Service Banking Scheduler', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('c2e3f4a5-3c2d-4e7f-8a5b-4c5d6e7f8a9b', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Credential API', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('d3f4a5b6-4d3e-4f8a-9b6c-5d6e7f8a9b0c', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Credential V1', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('e4a5b6c7-5e4f-4a9b-8c7d-6e7f8a9b0c1d', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'Member API', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('f5b6c7d8-6f5a-4b0c-9d8e-7f8a9b0c1d2e', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'OTP API', 'Tier-1', 'Service Banking service', NOW(), 'system')
ON CONFLICT DO NOTHING;

INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('3aa0c9b1-1b5f-4e4d-9d1a-2c3b4d5e6f70', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'SSO', 'Tier-1', 'Service Banking service', NOW(), 'system'),
('4bb1da12-2c6a-4f5e-8e2b-3d4c5e6f7081', 'ec81d79e-b13a-548e-8082-3c479facae8a', 'identity', 'Tier-1', 'Service Banking service', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- Payment 
INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim API', 'Tier-1', 'Payment service', NOW(), 'system'),
('2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim Batch Job', 'Tier-1', 'Payment service', NOW(), 'system'),
('3c4d5e6f-7a8b-4c9d-8e0f-2a3b4c5d6e7f', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim Batch Started Consumer', 'Tier-1', 'Payment service', NOW(), 'system'),
('4d5e6f7a-8b9c-4d0e-9f1a-3b4c5d6e7f8a', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim Create Ist. Card Batch Consumer', 'Tier-1', 'Payment service', NOW(), 'system'),
('5e6f7a8b-9c0d-4e1f-8a2b-4c5d6e7f8a9b', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim Org. Created Consumer', 'Tier-1', 'Payment service', NOW(), 'system'),
('6f7a8b9c-0d1e-4f2a-9b3c-5d6e7f8a9b0c', 'edb15541-d186-59eb-8876-d067098d2371', 'Belbim Transaction Failure Job', 'Tier-1', 'Payment service', NOW(), 'system'),
('7a8b9c0d-1e2f-4a3b-8c4d-6e7f8a9b0c1d', 'edb15541-d186-59eb-8876-d067098d2371', 'BKM V3', 'Tier-1', 'Payment service', NOW(), 'system'),
('8b9c0d1e-2f3a-4b4c-9d5e-7f8a9b0c1d2e', 'edb15541-d186-59eb-8876-d067098d2371', 'Mobile V5', 'Tier-1', 'Payment service', NOW(), 'system'),
('9c0d1e2f-3a4b-4c5d-8e6f-8a9b0c1d2e3f', 'edb15541-d186-59eb-8876-d067098d2371', 'Paydir V1', 'Tier-1', 'Payment service', NOW(), 'system'),
('a0b1c2d3-4e5f-4a6b-9c7d-9b0c1d2e3f4a', 'edb15541-d186-59eb-8876-d067098d2371', 'Payment API', 'Tier-1', 'Payment service', NOW(), 'system'),
('b1c2d3e4-5f6a-4b7c-8d8e-0c1d2e3f4a5b', 'edb15541-d186-59eb-8876-d067098d2371', 'Payment Consumer', 'Tier-1', 'Payment service', NOW(), 'system'),
('c2d3e4f5-6a7b-4c8d-9e9f-1d2e3f4a5b6c', 'edb15541-d186-59eb-8876-d067098d2371', 'Payment Integration', 'Tier-1', 'Payment service', NOW(), 'system'),
('d3e4f5a6-7b8c-4d9e-8a0b-2e3f4a5b6c7d', 'edb15541-d186-59eb-8876-d067098d2371', 'Payment V4', 'Tier-1', 'Payment service', NOW(), 'system'),
('e4f5a6b7-8c9d-4e0f-9b1c-3f4a5b6c7d8e', 'edb15541-d186-59eb-8876-d067098d2371', 'POS Terminal', 'Tier-1', 'Payment service', NOW(), 'system'),
('f5a6b7c8-9d0e-4f1a-8c2d-4a5b6c7d8e9f', 'edb15541-d186-59eb-8876-d067098d2371', 'POS Transaction', 'Tier-1', 'Payment service', NOW(), 'system'),
('0a1b2c3d-4e5f-4a6b-9c7d-5b6c7d8e9f0a', 'edb15541-d186-59eb-8876-d067098d2371', 'Recurring V1', 'Tier-1', 'Payment service', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- Common 
INSERT INTO services (id, owner_team_id, name, tier, description, created, created_by) VALUES
('11a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c', '51103da8-eb62-5383-935c-95c5f52a55a7', 'Aggregator V1', 'Tier-1', 'Common service', NOW(), 'system'),
('22b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d', '51103da8-eb62-5383-935c-95c5f52a55a7', 'Client API Gateway', 'Tier-1', 'Common service', NOW(), 'system'),
('33c4d5e6-f7a8-4b9c-8d0e-2f3a4b5c6d7e', '51103da8-eb62-5383-935c-95c5f52a55a7', 'File Server', 'Tier-1', 'Common service', NOW(), 'system'),
('44d5e6f7-a8b9-4c0d-9e1f-3a4b5c6d7e8f', '51103da8-eb62-5383-935c-95c5f52a55a7', 'OKC SVC', 'Tier-1', 'Common service', NOW(), 'system'),
('55e6f7a8-b9c0-4d1e-8a2b-4b5c6d7e8f9a', '51103da8-eb62-5383-935c-95c5f52a55a7', 'OKC V1', 'Tier-1', 'Common service', NOW(), 'system'),
('66f7a8b9-c0d1-4e2f-9b3c-5c6d7e8f9a0b', '51103da8-eb62-5383-935c-95c5f52a55a7', 'Payment Handler', 'Tier-1', 'Common service', NOW(), 'system'),
('77a8b9c0-d1e2-4f3a-8c4d-6d7e8f9a0b1c', '51103da8-eb62-5383-935c-95c5f52a55a7', 'Rest V5', 'Tier-1', 'Common service', NOW(), 'system'),
('88b9c0d1-e2f3-4a4b-9d5e-7e8f9a0b1c2d', '51103da8-eb62-5383-935c-95c5f52a55a7', 'Scheduler', 'Tier-1', 'Common service', NOW(), 'system')
ON CONFLICT DO NOTHING;

-- Link Services to Repositories
INSERT INTO service_repositories (id, service_id, repository_id, created, created_by)
SELECT gen_random_uuid(), s.id, r.id, NOW(), 'system'
FROM (
  VALUES
  -- Finance
  ('Accounting API', 'Accounting'),
  ('Accounting Consumer Transaction Summary Report', 'Accounting'),
  ('Bank Report Handler', 'odeal-bank-report-handler'),
  ('History', 'history'),
  ('Payback Callback Consumer', 'payback-callback-consumer'),
  ('Payfin Scheduler', 'payfin-scheduler'),
  ('Payment Matcher', 'odeal-payment-matcher'),
  ('Payout API', 'payout'),
  ('Payout Consumer', 'payout'),
  ('Payout Source API', 'payout-source'),
  ('Payout Source Handler', 'payout-source'),
  ('Report', 'Report'),
  ('THE V3', 'the'),
  ('TSM Handler', 'tsm-handler'),
  ('finance-client-api-gateway', 'finance-client-api-gateway'),
  ('transfer-api', 'Transfer'),
  ('transfer-consumer', 'Transfer'),
  ('transfer-scheduler', 'Transfer'),

  -- Service Banking
  ('Card Manager API', 'card-manager-v2'),
  ('Card Manager Consumer', 'card-manager-v2'),
  ('Credit Manager API', 'credit-manager'),
  ('Credit Manager Consumer', 'credit-manager'),
  ('Credit Manager Producer', 'credit-manager'),
  ('Fiba Gateway', 'fiba-gateway'),
  ('Fiba Integration', 'bank-integration'),
  ('Odeal Gateway', 'odeal-gateway'),
  ('Portal Backend', 'portal-backend'),
  ('Portal Manager', 'portal-manager'),
  ('Service Banking Scheduler', 'service-banking-scheduler'),
  ('Credential API', 'credential'),
  ('Credential V1', 'credential'),
  ('Member API', 'member'),
  ('OTP API', 'otp'),
  ('SSO', 'SSO'),
  ('identity', 'identity'),

  -- Payment
  ('Belbim API', 'belbim'),
  ('Belbim Batch Job', 'belbim'),
  ('Belbim Batch Started Consumer', 'belbim'),
  ('Belbim Create Ist. Card Batch Consumer', 'belbim'),
  ('Belbim Org. Created Consumer', 'belbim'),
  ('Belbim Transaction Failure Job', 'belbim'),
  ('BKM V3', 'bkm'),
  ('Mobile V5', 'mobile'),
  ('Payment API', 'payment'),
  ('Payment V4', 'payment'),
  ('Payment Consumer', 'odeal-consumers'),
  ('Payment Integration', 'Payment Integration'),
  ('Recurring V1', 'recurring'),

  -- Common
  ('Aggregator V1', 'aggregator'),
  ('Client API Gateway', 'client-api-gateway'),
  ('OKC SVC', 'okc-service'),
  ('OKC V1', 'okc'),
  ('Payment Handler', 'payment-handler'),
  ('Rest V5', 'rest'),
  ('Scheduler', 'scheduler')

) AS m(service_name, repo_name)
JOIN services s ON s.name = m.service_name
JOIN repositories r ON r.name = m.repo_name
ON CONFLICT DO NOTHING;

-- 9. Metric Thresholds
INSERT INTO metric_thresholds (id, metric_name, segment, min_value, max_value, level, created, created_by) VALUES
('9108a66b-a494-419a-b485-5297617ac406', 'deployment_frequency', 'default', 1.0, NULL, 'Elite', NOW(), 'system'),
('5b8e91d2-809b-4d14-a064-08b49bb8cb9b', 'deployment_frequency', 'default', 0.14, 1.0, 'High', NOW(), 'system'),
('49117782-71ff-4581-852a-f789751115fb', 'lead_time_for_changes', 'default', 0.0, 24.0, 'Elite', NOW(), 'system'),
('c3967cbb-9241-44d1-90a4-d0bbeeaca95e', 'lead_time_for_changes', 'default', 24.0, 168.0, 'High', NOW(), 'system'),
('61ab1d27-eca4-4306-b18f-ab22a275e8e7', 'time_to_restore_service', 'default', 0.0, 1.0, 'Elite', NOW(), 'system'),
('061faae6-a7c5-4559-ad37-2a397d41b163', 'time_to_restore_service', 'default', 1.0, 24.0, 'High', NOW(), 'system'),
('a9d228a1-8f87-4828-a724-b519b72db449', 'change_failure_rate', 'default', 0.0, 5.0, 'Elite', NOW(), 'system'),
('7bbd8a5d-2174-431a-9597-0ae7840362cf', 'change_failure_rate', 'default', 5.0, 15.0, 'High', NOW(), 'system'),

-- 1. Velocity & Flow
('a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'cycle_time', 'default', 0.0, 25.0, 'Elite', NOW(), 'system'), -- < 25 Hours
('b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'pickup_time', 'default', 0.0, 1.0, 'Elite', NOW(), 'system'), -- < 60 Mins (1 Hour)
('c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'review_time', 'default', 0.0, 3.0, 'Elite', NOW(), 'system'), -- < 3 Hours
('d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a', 'merge_frequency', 'default', 2.0, NULL, 'Elite', NOW(), 'system'), -- > 2.0 / Week
('e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b', 'flow_efficiency', 'default', 40.0, NULL, 'Elite', NOW(), 'system'), -- > 40%

-- 2. AI & Agentic
('f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c', 'ai_acceptance_rate', 'default', 30.0, NULL, 'Elite', NOW(), 'system'), -- > 30%
('a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d', 'agent_success_rate', 'default', 80.0, NULL, 'Elite', NOW(), 'system'), -- > 80%

-- 3. Quality & Risk
('b8c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e', 'pr_complexity', 'default', 0.0, 200.0, 'Elite', NOW(), 'system'), -- < 200 Lines
('c9d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f', 'rework_rate', 'default', 0.0, 3.0, 'Elite', NOW(), 'system'), -- < 3%
('d0e1f2a3-b4c5-4d6e-7f8a-9b0c1d2e3f4a', 'defect_density', 'default', 0.0, 0.2, 'Elite', NOW(), 'system'), -- < 0.2 per 1k lines
('e1f2a3b4-c5d6-4e7f-8a9b-0c1d2e3f4a5b', 'code_coverage', 'default', 80.0, NULL, 'Elite', NOW(), 'system') -- > 80%
ON CONFLICT DO NOTHING;
