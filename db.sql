create database ShortUrl;

create table urls(
	url_id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	original_url text not null,
	short_url text not null,
	clicks integer default 0,
	last_click timestamp,
	expiry_date date not null
)