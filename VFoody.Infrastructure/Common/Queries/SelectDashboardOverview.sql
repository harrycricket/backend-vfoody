﻿/*
 CreatedBy: ThongNV
 Date: 11/06/2024
 
 @DateFrom datetime
 @DateTo datetime
 */
-- SET @DateFrom:='2024-01-01';
-- SET @DateTo:='2024-07-01';

WITH TotalUser AS (
	SELECT
		COUNT(a.id) AS total_users
	FROM
		account a
	WHERE
		a.role_id IN (
			SELECT
				id
			FROM
				role
			WHERE
				name = "Customer"
				OR name = "Shop"
		)
		AND a.created_date BETWEEN @DateFrom
		AND @DateTo
),
TotalTrading AS (
	SELECT
		SUM(amount) AS total_trading
	FROM
		`transaction` t
	WHERE
		t.status = 1 -- PAID SUCCESS
		AND t.created_date BETWEEN @DateFrom
		AND @DateTo
),
TotalRevenue AS (
	SELECT
		(
			total_trading * (
				SELECT
					cc.commission_rate
				FROM
					commission_config cc
				ORDER BY
					cc.updated_date DESC
				LIMIT
					1
			) / 100
		) AS total_revenue
	FROM
		TotalTrading
),
TotalOrder AS (
	SELECT
		COUNT(o.id) AS total_order
	FROM
		`order` o
	WHERE
		o.status = 5 -- DELIVERIED
		AND o.created_date BETWEEN @DateFrom
		AND @DateTo
)
SELECT
	t.total_trading AS TotalTrading,
	(
		SELECT
            total_revenue
		FROM
			TotalRevenue
	) AS TotalRevenue,
	(
		SELECT
			total_order
		FROM
			TotalOrder
	) AS TotalOrder,
	(
		SELECT
			total_users
		FROM
			TotalUser
	) AS TotalUser
FROM
	TotalTrading AS t