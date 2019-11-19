/* Find all account statuses */
SELECT USERNAME, ACCOUNT_STATUS FROM DBA_USERS
WHERE ACCOUNT_STATUS LIKE '%LOCKED%'
ORDER BY USERNAME
;
 
/* Unlock account and reset password */
ALTER USER RMTOOLS_S90 IDENTIFIED BY rmtools_s90 ACCOUNT UNLOCK;
 
/*
If you unlock an account but do not reset the password then
the password remains expired. The first time someone
connects as that user, they must change the user's password.
*/
