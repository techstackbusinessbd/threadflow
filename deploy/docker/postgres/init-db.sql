-- ==============================================================================
-- ThreadFlow ERP - Database Initialization Script
-- PostgreSQL 16+ UUIDv7 & Keycloak DB Provisioning
-- ==============================================================================

-- 1. Create Keycloak Identity Database if not exists
SELECT 'CREATE DATABASE threadflow_keycloak'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'threadflow_keycloak')\gexec

-- 2. Connect to threadflow_db (default database)
\connect threadflow_db;

-- 3. Required Extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- 4. RFC 9562 UUIDv7 Generator Function
-- Generates sequential, millisecond-time-ordered UUIDs to eliminate B-tree index fragmentation
CREATE OR REPLACE FUNCTION uuid_generate_v7()
RETURNS uuid
AS $$
DECLARE
  unix_time_ms bytea;
  uuid_bytes bytea;
BEGIN
  -- Extract 48-bit timestamp in milliseconds from UTC clock
  unix_time_ms := substring(int8send(floor(extract(epoch FROM clock_timestamp()) * 1000)::bigint) FROM 3 FOR 6);
  -- Generate 16-byte buffer with timestamp prefix and 10 random bytes
  uuid_bytes := unix_time_ms || gen_random_bytes(10);
  -- Set version bits to 7 (0111) in byte 7
  uuid_bytes := set_byte(uuid_bytes, 6, (get_byte(uuid_bytes, 6) & 15) | 112);
  -- Set variant bits to RFC 9562 (10xx) in byte 9
  uuid_bytes := set_byte(uuid_bytes, 8, (get_byte(uuid_bytes, 8) & 63) | 128);
  RETURN encode(uuid_bytes, 'hex')::uuid;
END;
$$ LANGUAGE plpgsql VOLATILE;

-- 5. Verification Test
DO $$
DECLARE
  test_uuid uuid;
BEGIN
  test_uuid := uuid_generate_v7();
  RAISE NOTICE 'ThreadFlow PostgreSQL initialized. Sample UUIDv7 generated: %', test_uuid;
END $$;
