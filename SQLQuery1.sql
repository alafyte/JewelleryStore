USE jew_store_db;

-- логин: admin, пароль: admin1234

INSERT INTO USERS (GivenName, LastName, NickName, UserPasswordSalt, UserPasswordHash, Email, DateOfBirth, Access, isActive, Theme)
VALUES
(N'Имя', N'Фамилия', 'admin', 'TROIGb/NukRiZoNSuaeK0karLIZD4qlcgTNQ2JVEghA=', 'GD6fDLnTpkcoPnUS8nj7bnJ4SKrACLQWYWYa5s5+YeQ=', 'youremail@gmail.com', '2000-03-16',	'admin',  1, 'Theme1');


INSERT INTO PRODUCT_TYPE (PType) VALUES ('Necklace');
INSERT INTO PRODUCT_TYPE (PType) VALUES ('Ring');
INSERT INTO PRODUCT_TYPE (PType) VALUES ('Earring');
INSERT INTO PRODUCT_TYPE (PType) VALUES ('Wristwear');


INSERT INTO STONES (StoneNameEn, StoneNameRus)
VALUES 
  ('Diamond', N'Бриллиант'),
  ('Emerald', N'Изумруд'),
  ('Sapphire', N'Сапфир'),
  ('Ruby', N'Рубин'),
  ('Topaz', N'Топаз'),
  ('Amethyst', N'Аметист'),
  ('Opal', N'Опал'),
  ('Pearl', N'Жемчуг'),
  ('Aquamarine', N'Аквамарин'),
  ('Garnet', N'Гранат'),
  ('Jade', N'Нефрит'),
  ('Citrine', N'Цитрин'),
  ('Peridot', N'Перидот'),
  ('Tanzanite', N'Танзанит'),
  ('Tourmaline', N'Турмалин');

INSERT INTO METALS(MetalNameEn, MetalNameRus)
VALUES
('Gold', N'Золото'),
('Silver', N'Серебро'),
('Platinum', N'Платина');
