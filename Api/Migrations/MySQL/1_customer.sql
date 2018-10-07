CREATE TABLE customer (
  customer_id bigint UNSIGNED NOT NULL AUTO_INCREMENT,
  organization_name varchar(45) NOT NULL,
  PRIMARY KEY (customer_id),
  UNIQUE KEY customer_id_UNIQUE (customer_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO customer (organization_name) values ('Kipubit');