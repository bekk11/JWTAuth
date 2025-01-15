create table t_user(
    id bigserial primary key,
    username varchar(150) unique,
    password varchar(250),
    is_active bool default true
);

create table t_refresh_token(
    user_id bigint references t_user(id) unique,
    refresh_token varchar(50) unique,
    expiry timestamp
);