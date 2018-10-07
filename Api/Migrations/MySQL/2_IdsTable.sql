CREATE TABLE ids(
  entity_name VARCHAR(50) NOT NULL,
  next_high BIGINT UNSIGNED NOT NULL,
  PRIMARY KEY(entity_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO ids(entity_name, next_high) VALUES('Customer', 1);