CREATE SCHEMA IF NOT EXISTS currency_observer;

CREATE TABLE IF NOT EXISTS currency_observer.currency
(
    id            BIGINT           NOT NULL,
    currency_code INT              NOT NULL,
    value         DOUBLE PRECISION NOT NULL,
    name          VARCHAR(128)     NOT NULL,
    added_at      TIMESTAMP        NOT NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS added_at_id_index
    ON currency_observer.currency (added_at, id);

CREATE INDEX IF NOT EXISTS added_at_index 
    ON currency_observer.currency USING HASH (added_at);