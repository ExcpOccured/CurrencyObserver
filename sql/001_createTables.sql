CREATE SCHEMA IF NOT EXISTS currency_observer;

CREATE TABLE IF NOT EXISTS currency_observer.currency
(
    id            BIGINT           NOT NULL,
    currency_code INT              NOT NULL,
    value         DOUBLE PRECISION NOT NULL,
    name          VARCHAR(128)     NOT NULL,
    valid_date    TIMESTAMP        NOT NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS valid_date_id_index
    ON currency_observer.currency (valid_date, id);

CREATE INDEX IF NOT EXISTS valid_date_index 
    ON currency_observer.currency USING HASH (valid_date);