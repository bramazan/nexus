-- Clean up existing data (Optional: Use carefully)
-- TRUNCATE TABLE tool_accounts, team_members, service_repositories, services, repositories, teams, users, integrations, workspaces, metric_thresholds CASCADE;

-- 1. Workspaces
INSERT INTO workspaces (id, name, created_at) VALUES ('a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Ödeal Teknoloji', NOW() ON CONFLICT DO NOTHING;

-- 2. Integrations
INSERT INTO integrations (id, workspace_id, type, name, config, created_at) VALUES ('b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'jira', 'Main Jira Instance', '{"baseUrl": "https://odeal.atlassian.net", "email": "burak@ode.al", "apiToken": "YOUR_ATLASSIAN_API_TOKEN"}', NOW() ON CONFLICT DO NOTHING;
INSERT INTO integrations (id, workspace_id, type, name, config, created_at) VALUES ('c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'gitlab', 'Corporate GitLab', '{"baseUrl": "https://git.odeal.com", "accessToken": "Ai-qzOFf4-yhu5OWFIbQZm86MQp1OjFlCA.01.0y1laqe1t", "groupId": "3"}', NOW() ON CONFLICT DO NOTHING;

-- 3. Users
INSERT INTO users (id, full_name, email, created_at) VALUES
('96475646-0ddf-50dc-ab5b-dc498c2504db', 'Adem Taçyıldız', 'adem.tacyildiz@ode.al', NOW()),
('888d7286-7dba-5f62-826c-c4082ce66afa', 'Ahmet Sağlam', 'ahmet.saglam@ode.al', NOW()),
('f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'Alican İnan', 'alican.inan@ode.al', NOW()),
('09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'Anıl Akkaya', 'anil.akkaya@ode.al', NOW()),
('3fcd0f39-8866-5407-a193-bb81ceab588e', 'Anıl Sakaryalı', 'anil.sakaryali@ode.al', NOW()),
('411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'Burak Ramazan', 'burak@ode.al', NOW()),
('551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'Elif Burçak Namver', 'burcak.namver@ode.al', NOW()),
('590323a2-2dfc-579c-be0d-36791fbaed97', 'Erdem Öden', 'erdem.oden@ode.al', NOW()),
('880790c3-c4d8-53c0-beab-289f96a57053', 'Gökhan İbrikçi', 'gokhan.ibrikci@ode.al', NOW()),
('905e5226-83b8-5e1e-8053-a0fc570740e1', 'Hacı Burak Tahmaz', 'burak.tahmaz@ode.al', NOW()),
('6f0945ad-4bf5-58bf-8847-08f390ba7648', 'Hüseyin Mutlu', 'huseyin.mutlu@ode.al', NOW()),
('6eb987e8-d870-5604-bd5b-67a30d13b903', 'Mehmet Yetiş', 'mehmet.yetis@ode.al', NOW()),
('5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'Mert Kaim', 'mert.kaim@ode.al', NOW()),
('62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'Ahmet Umur Gültekin', 'umur.gultekin@ode.al', NOW()),
('8d113595-682d-5d60-9970-74b5087279ad', 'Metin İsfendiyar', 'metin.isfendiyar@ode.al', NOW()),
('8af193e1-1748-5b5f-9218-57593c0fd7e3', 'Mustafa Çolakoğlu', 'mustafa.colakoglu@ode.al', NOW()),
('51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'Resul Bozdemir', 'resul.bozdemir@ode.al', NOW()),
('9bb0636e-614a-5915-8d67-02dba568f12d', 'Volkan Kurt', 'volkan.kurt@ode.al', NOW()),
('73b5e99d-61bd-57b7-840b-0cece05fadb1', 'Yakup Doğan', 'yakup.dogan@ode.al', NOW()),
('c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'Yasir Arslan', 'yasir.arslan@ode.al', NOW()),
('659bd5b5-df2f-5aca-8977-2738d49196d1', 'Bilal Cihangir', 'bilal.cihangir@ode.al', NOW()),
('588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'Tahsin Civelek', 'tahsin.civelek@ode.al', NOW()),
('eed31354-2423-525b-8ed5-d4c5c3e58b78', 'Mustafa Hasan', 'mustafa.hasan@ode.al', NOW())
ON CONFLICT DO NOTHING;

-- 4. Teams
INSERT INTO teams (id, workspace_id, name, parent_team_id, created_at) VALUES ('51103da8-eb62-5383-935c-95c5f52a55a7', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Common', NULL, NOW() ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created_at) VALUES ('edb15541-d186-59eb-8876-d067098d2371', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW() ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created_at) VALUES ('81be54c0-c2e5-5790-8978-619825aa0263', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Finance', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW() ON CONFLICT DO NOTHING;
INSERT INTO teams (id, workspace_id, name, parent_team_id, created_at) VALUES ('ec81d79e-b13a-548e-8082-3c479facae8a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Service Banking', '51103da8-eb62-5383-935c-95c5f52a55a7', NOW() ON CONFLICT DO NOTHING;

-- 5. Team Members
INSERT INTO team_members (team_id, user_id, role, joined_at) VALUES
('81be54c0-c2e5-5790-8978-619825aa0263', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'Associate Software Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '888d7286-7dba-5f62-826c-c4082ce66afa', 'QA Engineer', NOW()),
('ec81d79e-b13a-548e-8082-3c479facae8a', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'Senior Software Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'QA Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'Software Engineer', NOW()),

('81be54c0-c2e5-5790-8978-619825aa0263', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'Senior Software Engineer', NOW()),
('81be54c0-c2e5-5790-8978-619825aa0263', '590323a2-2dfc-579c-be0d-36791fbaed97', 'Software Engineer', NOW()),
('ec81d79e-b13a-548e-8082-3c479facae8a', '880790c3-c4d8-53c0-beab-289f96a57053', 'Software Development Team Lead', NOW()),
('ec81d79e-b13a-548e-8082-3c479facae8a', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'QA Engineer', NOW()),
('81be54c0-c2e5-5790-8978-619825aa0263', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'Associate Software Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'QA Engineer', NOW()),
('ec81d79e-b13a-548e-8082-3c479facae8a', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'Software Engineer', NOW()),
('ec81d79e-b13a-548e-8082-3c479facae8a', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'Product Owner', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '8d113595-682d-5d60-9970-74b5087279ad', 'Technical Analyst', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'Software Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'Software Engineer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '9bb0636e-614a-5915-8d67-02dba568f12d', 'Software Architect', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'QA Developer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'Stajyer', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'Product Owner', NOW()),
('edb15541-d186-59eb-8876-d067098d2371', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'Product Owner', NOW()),
('81be54c0-c2e5-5790-8978-619825aa0263', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'Product Owner', NOW())
ON CONFLICT DO NOTHING;

-- 6. Tool Accounts (GitLab username & Jira email)
INSERT INTO tool_accounts (id, user_id, integration_id, external_id, username, external_email, display_name, external_metadata, is_active, created_at, created_by) VALUES
('853f7b7f-76eb-4529-97cb-1b174c532b05', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'adem.tacyildiz', 'adem.tacyildiz', 'adem.tacyildiz', 'adem.tacyildiz', '{}', true, NOW()), 'system'),
('13d7a3da-211d-433a-b7e9-4d1819bdf1e4', '96475646-0ddf-50dc-ab5b-dc498c2504db', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'adem.tacyildiz@ode.al', 'adem.tacyildiz@ode.al', 'adem.tacyildiz@ode.al', 'adem.tacyildiz@ode.al', '{}', true, NOW()), 'system'),
('7a6c234d-ec2c-40d1-8f26-9568d7cd8361', '888d7286-7dba-5f62-826c-c4082ce66afa', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'ahmet.saglam', 'ahmet.saglam', 'ahmet.saglam', 'ahmet.saglam', '{}', true, NOW()), 'system'),
('b252377f-7771-4074-a424-e97543076eba', '888d7286-7dba-5f62-826c-c4082ce66afa', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'ahmet.saglam@ode.al', 'ahmet.saglam@ode.al', 'ahmet.saglam@ode.al', 'ahmet.saglam@ode.al', '{}', true, NOW()), 'system'),
('3f30fb7e-c188-489b-988a-fd648c2d2744', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'alican.inan', 'alican.inan', 'alican.inan', 'alican.inan', '{}', true, NOW()), 'system'),
('eeb7989f-a95d-45ce-87e3-c142a0d79a2d', 'f2cbe571-0cb8-5d82-bbbf-e704081c8dda', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'alican.inan@ode.al', 'alican.inan@ode.al', 'alican.inan@ode.al', 'alican.inan@ode.al', '{}', true, NOW()), 'system'),
('223ac40e-de95-4724-b278-193fe358ef69', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'anil.akkaya', 'anil.akkaya', 'anil.akkaya', 'anil.akkaya', '{}', true, NOW()), 'system'),
('73d5d49f-cfd9-4175-be9a-055864156606', '09e0ebb8-61b0-5aff-b02c-22c0c8785652', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'anil.akkaya@ode.al', 'anil.akkaya@ode.al', 'anil.akkaya@ode.al', 'anil.akkaya@ode.al', '{}', true, NOW()), 'system'),
('d3143f12-f913-481e-b9a7-3b5caa890e7f', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'anil.sakaryali', 'anil.sakaryali', 'anil.sakaryali', 'anil.sakaryali', '{}', true, NOW()), 'system'),
('ad8b8a9f-0bde-430d-89f7-b0e171c285b3', '3fcd0f39-8866-5407-a193-bb81ceab588e', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'anil.sakaryali@ode.al', 'anil.sakaryali@ode.al', 'anil.sakaryali@ode.al', 'anil.sakaryali@ode.al', '{}', true, NOW()), 'system'),
('a4d65fc6-d806-49ff-b86d-a756687bef0a', '411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burak', 'burak', 'burak', 'burak', '{}', true, NOW()), 'system'),
('f97a58e7-448e-44f9-a512-61d9c33cf2d3', '411d2cd1-8db0-5243-bf17-2de2b35fa3ce', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burak@ode.al', 'burak@ode.al', 'burak@ode.al', 'burak@ode.al', '{}', true, NOW()), 'system'),
('152d2cf8-c4ef-4c29-be46-bc201539e2a2', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burcak.namver', 'burcak.namver', 'burcak.namver', 'burcak.namver', '{}', true, NOW()), 'system'),
('eaaf9996-8c87-4d94-8a19-eab6b40a65af', '551e9bed-471e-5f5d-bd9a-6bb5e3e29b4a', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burcak.namver@ode.al', 'burcak.namver@ode.al', 'burcak.namver@ode.al', 'burcak.namver@ode.al', '{}', true, NOW()), 'system'),
('cd80d5a4-ebe6-4bd2-a79e-7a3bd9a45968', '590323a2-2dfc-579c-be0d-36791fbaed97', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'erdem.oden', 'erdem.oden', 'erdem.oden', 'erdem.oden', '{}', true, NOW()), 'system'),
('9595e784-7354-489b-8d0c-bef55026a6d8', '590323a2-2dfc-579c-be0d-36791fbaed97', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'erdem.oden@ode.al', 'erdem.oden@ode.al', 'erdem.oden@ode.al', 'erdem.oden@ode.al', '{}', true, NOW()), 'system'),
('02159c45-ebd8-4a78-ba77-c7b4221dc8b8', '880790c3-c4d8-53c0-beab-289f96a57053', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'gokhan.ibrikci', 'gokhan.ibrikci', 'gokhan.ibrikci', 'gokhan.ibrikci', '{}', true, NOW()), 'system'),
('83cba3d9-790f-4aa8-80c8-36cb40a8c74f', '880790c3-c4d8-53c0-beab-289f96a57053', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'gokhan.ibrikci@ode.al', 'gokhan.ibrikci@ode.al', 'gokhan.ibrikci@ode.al', 'gokhan.ibrikci@ode.al', '{}', true, NOW()), 'system'),
('39b88c63-984e-4dda-b09b-d09cedc0bcad', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'burak.tahmaz', 'burak.tahmaz', 'burak.tahmaz', 'burak.tahmaz', '{}', true, NOW()), 'system'),
('a806236e-ec36-417d-adab-9f6941065cd7', '905e5226-83b8-5e1e-8053-a0fc570740e1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'burak.tahmaz@ode.al', 'burak.tahmaz@ode.al', 'burak.tahmaz@ode.al', 'burak.tahmaz@ode.al', '{}', true, NOW()), 'system'),
('558f6086-adb7-4772-9830-8ab8777e3ab4', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'huseyin.mutlu', 'huseyin.mutlu', 'huseyin.mutlu', 'huseyin.mutlu', '{}', true, NOW()), 'system'),
('3ab33345-cd17-4d59-b918-3671958387ab', '6f0945ad-4bf5-58bf-8847-08f390ba7648', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'huseyin.mutlu@ode.al', 'huseyin.mutlu@ode.al', 'huseyin.mutlu@ode.al', 'huseyin.mutlu@ode.al', '{}', true, NOW()), 'system'),
('c8cbf118-75c0-4591-99c4-d915eda1c595', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mehmet.yetis', 'mehmet.yetis', 'mehmet.yetis', 'mehmet.yetis', '{}', true, NOW()), 'system'),
('256446cc-9610-415c-94ce-d6af65c97136', '6eb987e8-d870-5604-bd5b-67a30d13b903', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mehmet.yetis@ode.al', 'mehmet.yetis@ode.al', 'mehmet.yetis@ode.al', 'mehmet.yetis@ode.al', '{}', true, NOW()), 'system'),
('a21ccf24-dcf8-4ac2-841d-757748c27a87', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mert.kaim', 'mert.kaim', 'mert.kaim', 'mert.kaim', '{}', true, NOW()), 'system'),
('8c623d26-10cc-4e0f-b4b3-e67f1bf8abe6', '5cc8148c-f0a5-5e2c-8720-afdd8335456f', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mert.kaim@ode.al', 'mert.kaim@ode.al', 'mert.kaim@ode.al', 'mert.kaim@ode.al', '{}', true, NOW()), 'system'),
('732d9d98-0e84-4219-8947-b1d7534a1c75', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'umur.gultekin', 'umur.gultekin', 'umur.gultekin', 'umur.gultekin', '{}', true, NOW()), 'system'),
('bf970b2d-0959-4392-b58e-2272471d30a6', '62174471-38b8-514d-b1da-b8d8bf6cbfa1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'umur.gultekin@ode.al', 'umur.gultekin@ode.al', 'umur.gultekin@ode.al', 'umur.gultekin@ode.al', '{}', true, NOW()), 'system'),
('21f82150-94b4-4535-8d87-283eefa782f8', '8d113595-682d-5d60-9970-74b5087279ad', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'metin.isfendiyar', 'metin.isfendiyar', 'metin.isfendiyar', 'metin.isfendiyar', '{}', true, NOW()), 'system'),
('95ff60f9-ac45-414e-9d9d-6b507b4c2fae', '8d113595-682d-5d60-9970-74b5087279ad', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'metin.isfendiyar@ode.al', 'metin.isfendiyar@ode.al', 'metin.isfendiyar@ode.al', 'metin.isfendiyar@ode.al', '{}', true, NOW()), 'system'),
('6f731f1b-fbed-412e-9960-4cbbe0f28ff1', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mustafa.colakoglu', 'mustafa.colakoglu', 'mustafa.colakoglu', 'mustafa.colakoglu', '{}', true, NOW()), 'system'),
('8188ed47-22ce-4cc9-9036-ba92b81b5ec5', '8af193e1-1748-5b5f-9218-57593c0fd7e3', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mustafa.colakoglu@ode.al', 'mustafa.colakoglu@ode.al', 'mustafa.colakoglu@ode.al', 'mustafa.colakoglu@ode.al', '{}', true, NOW()), 'system'),
('43815d6e-55dd-4ffd-b83e-ecb9711f01b7', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'resul.bozdemir', 'resul.bozdemir', 'resul.bozdemir', 'resul.bozdemir', '{}', true, NOW()), 'system'),
('1fa2f69d-e9ee-4e33-b5d6-86ac5a5a9139', '51c48b20-dafd-5adf-a3c1-2cea08ad12ca', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'resul.bozdemir@ode.al', 'resul.bozdemir@ode.al', 'resul.bozdemir@ode.al', 'resul.bozdemir@ode.al', '{}', true, NOW()), 'system'),
('833382d1-f500-4649-81e7-f905674d3705', '9bb0636e-614a-5915-8d67-02dba568f12d', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'volkan.kurt', 'volkan.kurt', 'volkan.kurt', 'volkan.kurt', '{}', true, NOW()), 'system'),
('7c4ff69a-d018-405d-962d-d647db89258d', '9bb0636e-614a-5915-8d67-02dba568f12d', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'volkan.kurt@ode.al', 'volkan.kurt@ode.al', 'volkan.kurt@ode.al', 'volkan.kurt@ode.al', '{}', true, NOW()), 'system'),
('08be156b-73a3-4272-8b34-3c72e73eb917', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'yakup.dogan', 'yakup.dogan', 'yakup.dogan', 'yakup.dogan', '{}', true, NOW()), 'system'),
('052e7278-989b-4548-a9bb-db7623a0344c', '73b5e99d-61bd-57b7-840b-0cece05fadb1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'yakup.dogan@ode.al', 'yakup.dogan@ode.al', 'yakup.dogan@ode.al', 'yakup.dogan@ode.al', '{}', true, NOW()), 'system'),
('713526c4-ddff-4f5f-8917-584fdd217089', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'yasir.arslan', 'yasir.arslan', 'yasir.arslan', 'yasir.arslan', '{}', true, NOW()), 'system'),
('6de495f0-b90d-4f40-ade8-514d7face96e', 'c57627f4-9a6a-5581-b2d1-be48be4d16a4', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'yasir.arslan@ode.al', 'yasir.arslan@ode.al', 'yasir.arslan@ode.al', 'yasir.arslan@ode.al', '{}', true, NOW()), 'system'),
('6a9b2ec9-aa86-43c1-94be-8b8790524ed2', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bilal.cihangir', 'bilal.cihangir', 'bilal.cihangir', 'bilal.cihangir', '{}', true, NOW()), 'system'),
('e505e2cd-5afc-4055-af72-e79aaa218e98', '659bd5b5-df2f-5aca-8977-2738d49196d1', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'bilal.cihangir@ode.al', 'bilal.cihangir@ode.al', 'bilal.cihangir@ode.al', 'bilal.cihangir@ode.al', '{}', true, NOW()), 'system'),
('9a4dd7da-89b6-4a41-a24d-12a17dcf72b0', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'tahsin.civelek', 'tahsin.civelek', 'tahsin.civelek', 'tahsin.civelek', '{}', true, NOW()), 'system'),
('a479bb72-2ad6-429b-af1d-84c310c75a56', '588e1eda-29e8-5101-a8a3-1a1cd7ae0487', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'tahsin.civelek@ode.al', 'tahsin.civelek@ode.al', 'tahsin.civelek@ode.al', 'tahsin.civelek@ode.al', '{}', true, NOW()), 'system'),
('5eaec70c-dae1-4fb5-bdac-15a9a4fd66f6', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mustafa.hasan', 'mustafa.hasan', 'mustafa.hasan', 'mustafa.hasan', '{}', true, NOW()), 'system'),
('cca981ed-636c-49f6-bf48-d5831512a7b3', 'eed31354-2423-525b-8ed5-d4c5c3e58b78', 'b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'mustafa.hasan@ode.al', 'mustafa.hasan@ode.al', 'mustafa.hasan@ode.al', 'mustafa.hasan@ode.al', '{}', true, NOW()), 'system')
ON CONFLICT DO NOTHING;

-- 7. Repositories
-- Finance
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, created_at, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'finance-client-api-gateway', '514', 'https://git.odeal.com/odeal/finance/finance-client-api-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Transfer', '512', 'https://git.odeal.com/odeal/finance/transfer', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'finance-matching-app', '468', 'https://git.odeal.com/odeal/finance/finance-matching-app', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Accounting', '467', 'https://git.odeal.com/odeal/finance/Accounting', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Report', '466', 'https://git.odeal.com/odeal/finance/report', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'the', '450', 'https://git.odeal.com/odeal/finance/the', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payfin-scheduler', '449', 'https://git.odeal.com/odeal/finance/payfin-scheduler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payout-source', '442', 'https://git.odeal.com/odeal/finance/payout-source', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-payment-matcher', '440', 'https://git.odeal.com/odeal/finance/odeal-payment-matcher', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'history', '424', 'https://git.odeal.com/odeal/finance/history', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'tsm-handler', '423', 'https://git.odeal.com/odeal/finance/tsm-handler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-bank-report-handler', '422', 'https://git.odeal.com/odeal/finance/odeal-bank-report-handler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payback-callback-consumer', '382', 'https://git.odeal.com/odeal/finance/payback-callback-consumer', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payout', '67', 'https://git.odeal.com/odeal/finance/payout', 'main', NOW()), NULL),
ON CONFLICT DO NOTHING;

-- Payment
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, created_at, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Docs', '619', 'https://git.odeal.com/odeal/payment/docs', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'recurring', '455', 'https://git.odeal.com/odeal/payment/recurring', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment-gateway', '444', 'https://git.odeal.com/odeal/payment/payment-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bkm', '432', 'https://git.odeal.com/odeal/payment/bkm', 'master', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'belbim', '431', 'https://git.odeal.com/odeal/payment/belbim', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment', '430', 'https://git.odeal.com/odeal/payment/payment', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'vpos-payment-gateway', '425', 'https://git.odeal.com/odeal/payment/vpos-payment-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mobile', '417', 'https://git.odeal.com/odeal/payment/mobile', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'pos-gateway', '332', 'https://git.odeal.com/odeal/payment/pos-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Payment Integration', '317', 'https://git.odeal.com/odeal/payment/payment-integration', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-consumers', '213', 'https://git.odeal.com/odeal/payment/odeal-consumers', 'master', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential-service', '118', 'https://git.odeal.com/odeal/payment/credential-service', 'master', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-fraud-old', '56', 'https://git.odeal.com/odeal/payment/odeal-fraud-old', 'master', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'mobile-gateway', '44', 'https://git.odeal.com/odeal/payment/mobile-gateway', 'master', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'Basket Test App', '19', 'https://git.odeal.com/odeal/payment/basket-test-app', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payfront-test-project', '4', 'https://git.odeal.com/odeal/payment/payfront-test-project', 'main', NOW()), NULL),
ON CONFLICT DO NOTHING;

-- Service Banking
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, created_at, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'card-manager-v2', '639', 'https://git.odeal.com/odeal/service-banking/card-manager-v2', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'SSO', '628', 'https://git.odeal.com/odeal/service-banking/sso-odeal', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'identity', '615', 'https://git.odeal.com/odeal/service-banking/identity', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'bank-integration', '568', 'https://git.odeal.com/odeal/service-banking/fiba-integration', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'odeal-gateway', '566', 'https://git.odeal.com/odeal/service-banking/odeal-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credit-manager', '551', 'https://git.odeal.com/odeal/service-banking/credit-manager', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'portal-manager', '545', 'https://git.odeal.com/odeal/service-banking/portal-manager', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'card-manager', '436', 'https://git.odeal.com/odeal/service-banking/card-manager', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'fiba-gateway', '428', 'https://git.odeal.com/odeal/service-banking/fiba-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'service-banking-scheduler', '420', 'https://git.odeal.com/odeal/service-banking/service-banking-scheduler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential-service', '404', 'https://git.odeal.com/odeal/service-banking/credential-service', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'member', '402', 'https://git.odeal.com/odeal/service-banking/member', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'otp', '383', 'https://git.odeal.com/odeal/service-banking/otp', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'credential', '363', 'https://git.odeal.com/odeal/service-banking/credential', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'portal-backend', '333', 'https://git.odeal.com/odeal/service-banking/portal-backend', 'main', NOW()), NULL),
ON CONFLICT DO NOTHING;

-- Common 
INSERT INTO repositories
  (id, integration_id, name, external_id, url, default_branch, created_at, created_by)
VALUES
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'client-api-gateway', '465', 'https://git.odeal.com/odeal/common/client-api-gateway', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'scheduler', '456', 'https://git.odeal.com/odeal/common/scheduler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'rest', '454', 'https://git.odeal.com/odeal/common/rest', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'okc-service', '448', 'https://git.odeal.com/odeal/common/okc-service', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'payment-handler', '445', 'https://git.odeal.com/odeal/common/payment-handler', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'okc', '443', 'https://git.odeal.com/odeal/common/okc', 'main', NOW()), NULL),
  (gen_random_uuid(), 'c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'aggregator', '433', 'https://git.odeal.com/odeal/common/aggregator', 'main', NOW()), NULL),
ON CONFLICT DO NOTHING;

-- 8. Services

-- Finance
INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('c4ed2a4b-5d35-4d28-8f2c-0d2c8e43c9e1', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Accounting API', 'Tier-1', 'Finance service', NOW()),
('c3c2e3bd-3c4c-4d5b-8f87-2b69e1f56c9a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Accounting Consumer Transaction Summary Report', 'Tier-1', 'Finance service', NOW()),
('b1d8b7a2-8a2b-4b7a-9b2d-1a9f2e0b3d4c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Bank Report Handler', 'Tier-1', 'Finance service', NOW()),
('f2c4c6a1-1a5e-4c0b-9e5d-7f6d8b9a0c12', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'History', 'Tier-1', 'Finance service', NOW()),
('d7a3f0e4-6b7c-4c2b-8a1d-3e5f6a7b8c9d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payback Callback Consumer', 'Tier-1', 'Finance service', NOW()),
('a8c9d0e1-2f3b-4a5c-9d6e-7b8c9a0d1e2f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payback Consumer', 'Tier-1', 'Finance service', NOW()),
('e1f2a3b4-5c6d-4e7f-8a9b-0c1d2e3f4a5b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payback V1', 'Tier-1', 'Finance service', NOW()),
('b2c3d4e5-6f7a-4b8c-9d0e-1f2a3b4c5d6e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payfin Scheduler', 'Tier-1', 'Finance service', NOW()),
('c5d6e7f8-9a0b-4c1d-8e2f-3a4b5c6d7e8f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment Matcher', 'Tier-1', 'Finance service', NOW()),
('a1b2c3d4-5e6f-4a7b-8c9d-0e1f2a3b4c5d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payout API', 'Tier-1', 'Finance service', NOW()),
('f0e1d2c3-b4a5-4c6d-8e7f-9a0b1c2d3e4f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payout Consumer', 'Tier-1', 'Finance service', NOW()),
('d1e2f3a4-b5c6-4d7e-8f9a-0b1c2d3e4f5a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payout Source API', 'Tier-1', 'Finance service', NOW()),
('e2f3a4b5-c6d7-4e8f-9a0b-1c2d3e4f5a6b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payout Source Handler', 'Tier-1', 'Finance service', NOW()),
('f3a4b5c6-d7e8-4f9a-0b1c-2d3e4f5a6b7c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Report', 'Tier-1', 'Finance service', NOW()),
('a4b5c6d7-e8f9-4a0b-1c2d-3e4f5a6b7c8d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'THE V3', 'Tier-1', 'Finance service', NOW()),
('b5c6d7e8-f9a0-4b1c-2d3e-4f5a6b7c8d9e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'TSM Handler', 'Tier-1', 'Finance service', NOW())
ON CONFLICT DO NOTHING;

INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('8f1f2a3b-4c5d-4e6f-8a9b-0c1d2e3f4a5b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'finance-client-api-gateway', 'Tier-1', 'Finance service', NOW()),
('9a2b3c4d-5e6f-4a7b-8c9d-1e2f3a4b5c6d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'transfer-api', 'Tier-1', 'Finance service', NOW()),
('0b3c4d5e-6f7a-4b8c-9d0e-2f3a4b5c6d7e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'transfer-consumer', 'Tier-1', 'Finance service', NOW()),
('1c4d5e6f-7a8b-4c9d-8e0f-3a4b5c6d7e8f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'transfer-scheduler', 'Tier-1', 'Finance service', NOW())
ON CONFLICT DO NOTHING;

-- Service Banking
INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('1f9a7c3d-2a1b-4c8e-9b7c-3f5a6b7c8d9e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Card Manager API', 'Tier-1', 'Service Banking service', NOW()),
('2a8b6c4d-3e2f-4a9b-8c7d-4e5f6a7b8c9d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Card Manager Consumer', 'Tier-1', 'Service Banking service', NOW()),
('3b7c5d4e-4f3a-4b8c-9d6e-5f6a7b8c9d0e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Credit Manager API', 'Tier-1', 'Service Banking service', NOW()),
('4c6d5e7f-5a4b-4c9d-8e7f-6a7b8c9d0e1f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Fiba Gateway', 'Tier-1', 'Service Banking service', NOW()),
('5d7e6f8a-6b5c-4d0e-9f8a-7b8c9d0e1f2a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Fiba Integration', 'Tier-1', 'Service Banking service', NOW()),
('6e8f7a9b-7c6d-4e1f-8a9b-8c9d0e1f2a3b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Odeal Gateway', 'Tier-1', 'Service Banking service', NOW()),
('7f9a8b0c-8d7e-4f2a-9b0c-9d0e1f2a3b4c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Portal Backend', 'Tier-1', 'Service Banking service', NOW()),
('8a0b9c1d-9e8f-4a3b-8c1d-0e1f2a3b4c5d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Portal Manager', 'Tier-1', 'Service Banking service', NOW()),
('9b1c0d2e-0f9a-4b4c-9d2e-1f2a3b4c5d6e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Credit Manager Consumer', 'Tier-1', 'Service Banking service', NOW()),
('a0c1d2e3-1a0b-4c5d-8e3f-2a3b4c5d6e7f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Credit Manager Producer', 'Tier-1', 'Service Banking service', NOW()),
('b1d2e3f4-2b1c-4d6e-9f4a-3b4c5d6e7f8a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Service Banking Scheduler', 'Tier-1', 'Service Banking service', NOW()),
('c2e3f4a5-3c2d-4e7f-8a5b-4c5d6e7f8a9b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Credential API', 'Tier-1', 'Service Banking service', NOW()),
('d3f4a5b6-4d3e-4f8a-9b6c-5d6e7f8a9b0c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Credential V1', 'Tier-1', 'Service Banking service', NOW()),
('e4a5b6c7-5e4f-4a9b-8c7d-6e7f8a9b0c1d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Member API', 'Tier-1', 'Service Banking service', NOW()),
('f5b6c7d8-6f5a-4b0c-9d8e-7f8a9b0c1d2e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'OTP API', 'Tier-1', 'Service Banking service', NOW())
ON CONFLICT DO NOTHING;

INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('3aa0c9b1-1b5f-4e4d-9d1a-2c3b4d5e6f70', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'SSO', 'Tier-1', 'Service Banking service', NOW()),
('4bb1da12-2c6a-4f5e-8e2b-3d4c5e6f7081', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'identity', 'Tier-1', 'Service Banking service', NOW())
ON CONFLICT DO NOTHING;

-- Payment 
INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim API', 'Tier-1', 'Payment service', NOW()),
('2b3c4d5e-6f7a-4b8c-9d0e-1f2a3b4c5d6e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim Batch Job', 'Tier-1', 'Payment service', NOW()),
('3c4d5e6f-7a8b-4c9d-8e0f-2a3b4c5d6e7f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim Batch Started Consumer', 'Tier-1', 'Payment service', NOW()),
('4d5e6f7a-8b9c-4d0e-9f1a-3b4c5d6e7f8a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim Create Ist. Card Batch Consumer', 'Tier-1', 'Payment service', NOW()),
('5e6f7a8b-9c0d-4e1f-8a2b-4c5d6e7f8a9b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim Org. Created Consumer', 'Tier-1', 'Payment service', NOW()),
('6f7a8b9c-0d1e-4f2a-9b3c-5d6e7f8a9b0c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Belbim Transaction Failure Job', 'Tier-1', 'Payment service', NOW()),
('7a8b9c0d-1e2f-4a3b-8c4d-6e7f8a9b0c1d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'BKM V3', 'Tier-1', 'Payment service', NOW()),
('8b9c0d1e-2f3a-4b4c-9d5e-7f8a9b0c1d2e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Mobile V5', 'Tier-1', 'Payment service', NOW()),
('9c0d1e2f-3a4b-4c5d-8e6f-8a9b0c1d2e3f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Paydir V1', 'Tier-1', 'Payment service', NOW()),
('a0b1c2d3-4e5f-4a6b-9c7d-9b0c1d2e3f4a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment API', 'Tier-1', 'Payment service', NOW()),
('b1c2d3e4-5f6a-4b7c-8d8e-0c1d2e3f4a5b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment Consumer', 'Tier-1', 'Payment service', NOW()),
('c2d3e4f5-6a7b-4c8d-9e9f-1d2e3f4a5b6c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment Integration', 'Tier-1', 'Payment service', NOW()),
('d3e4f5a6-7b8c-4d9e-8a0b-2e3f4a5b6c7d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment V4', 'Tier-1', 'Payment service', NOW()),
('e4f5a6b7-8c9d-4e0f-9b1c-3f4a5b6c7d8e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'POS Terminal', 'Tier-1', 'Payment service', NOW()),
('f5a6b7c8-9d0e-4f1a-8c2d-4a5b6c7d8e9f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'POS Transaction', 'Tier-1', 'Payment service', NOW()),
('0a1b2c3d-4e5f-4a6b-9c7d-5b6c7d8e9f0a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Recurring V1', 'Tier-1', 'Payment service', NOW())
ON CONFLICT DO NOTHING;

-- Common 
INSERT INTO services (id, workspace_id, name, tier, description, created_at) VALUES
('11a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Aggregator V1', 'Tier-1', 'Common service', NOW()),
('22b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Client API Gateway', 'Tier-1', 'Common service', NOW()),
('33c4d5e6-f7a8-4b9c-8d0e-2f3a4b5c6d7e', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'File Server', 'Tier-1', 'Common service', NOW()),
('44d5e6f7-a8b9-4c0d-9e1f-3a4b5c6d7e8f', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'OKC SVC', 'Tier-1', 'Common service', NOW()),
('55e6f7a8-b9c0-4d1e-8a2b-4b5c6d7e8f9a', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'OKC V1', 'Tier-1', 'Common service', NOW()),
('66f7a8b9-c0d1-4e2f-9b3c-5c6d7e8f9a0b', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Payment Handler', 'Tier-1', 'Common service', NOW()),
('77a8b9c0-d1e2-4f3a-8c4d-6d7e8f9a0b1c', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Rest V5', 'Tier-1', 'Common service', NOW()),
('88b9c0d1-e2f3-4a4b-9d5e-7e8f9a0b1c2d', 'a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'Scheduler', 'Tier-1', 'Common service', NOW())
ON CONFLICT DO NOTHING;

-- Link Services to Repositories
INSERT INTO service_repositories (service_id, repository_id)
SELECT s.id, r.id
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
INSERT INTO metric_thresholds (id, metric_name, segment, min_value, max_value, level, created_at) VALUES
('9108a66b-a494-419a-b485-5297617ac406', 'deployment_frequency', 'default', 1.0, NULL, 'Elite', NOW()),
('5b8e91d2-809b-4d14-a064-08b49bb8cb9b', 'deployment_frequency', 'default', 0.14, 1.0, 'High', NOW()),
('49117782-71ff-4581-852a-f789751115fb', 'lead_time_for_changes', 'default', 0.0, 24.0, 'Elite', NOW()),
('c3967cbb-9241-44d1-90a4-d0bbeeaca95e', 'lead_time_for_changes', 'default', 24.0, 168.0, 'High', NOW()),
('61ab1d27-eca4-4306-b18f-ab22a275e8e7', 'time_to_restore_service', 'default', 0.0, 1.0, 'Elite', NOW()),
('061faae6-a7c5-4559-ad37-2a397d41b163', 'time_to_restore_service', 'default', 1.0, 24.0, 'High', NOW()),
('a9d228a1-8f87-4828-a724-b519b72db449', 'change_failure_rate', 'default', 0.0, 5.0, 'Elite', NOW()),
('7bbd8a5d-2174-431a-9597-0ae7840362cf', 'change_failure_rate', 'default', 5.0, 15.0, 'High', NOW()),

-- 1. Velocity & Flow
('a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d', 'cycle_time', 'default', 0.0, 25.0, 'Elite', NOW()), -- < 25 Hours
('b2c3d4e5-f6a7-4b5c-8d9e-1f2a3b4c5d6e', 'pickup_time', 'default', 0.0, 1.0, 'Elite', NOW()), -- < 60 Mins (1 Hour)
('c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f', 'review_time', 'default', 0.0, 3.0, 'Elite', NOW()), -- < 3 Hours
('d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a', 'merge_frequency', 'default', 2.0, NULL, 'Elite', NOW()), -- > 2.0 / Week
('e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b', 'flow_efficiency', 'default', 40.0, NULL, 'Elite', NOW()), -- > 40%

-- 2. AI & Agentic
('f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c', 'ai_acceptance_rate', 'default', 30.0, NULL, 'Elite', NOW()), -- > 30%
('a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d', 'agent_success_rate', 'default', 80.0, NULL, 'Elite', NOW()), -- > 80%

-- 3. Quality & Risk
('b8c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e', 'pr_complexity', 'default', 0.0, 200.0, 'Elite', NOW()), -- < 200 Lines
('c9d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f', 'rework_rate', 'default', 0.0, 3.0, 'Elite', NOW()), -- < 3%
('d0e1f2a3-b4c5-4d6e-7f8a-9b0c1d2e3f4a', 'defect_density', 'default', 0.0, 0.2, 'Elite', NOW()), -- < 0.2 per 1k lines
('e1f2a3b4-c5d6-4e7f-8a9b-0c1d2e3f4a5b', 'code_coverage', 'default', 80.0, NULL, 'Elite', NOW() -- > 80%
ON CONFLICT DO NOTHING;
