/* Find all account statuses */
SELECT USERNAME, ACCOUNT_STATUS FROM DBA_USERS
WHERE ACCOUNT_STATUS LIKE '%LOCKED%'
ORDER BY USERNAME
;
 
/* Unlock account and reset password */
ALTER USER A_USER_NAME IDENTIFIED BY a_user_name ACCOUNT UNLOCK;
 
/*
If you unlock an account but do not reset the password then
the password remains expired. The first time someone
connects as that user, they must change the user's password.
*/

/* To avoid password expire set reuse and life time to unlimited */
alter profile DEFAULT limit PASSWORD_REUSE_TIME unlimited;

alter profile DEFAULT limit PASSWORD_LIFE_TIME  unlimited;
