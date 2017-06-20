import common_mail
import sys

if sys.argv == 1:
    print 'args not provided'
    exit(1)

arguments = sys.argv[1].split(';')
print(arguments)
to_list = arguments[0].split(',')
invitation_subject = arguments[1]
start_time = arguments[2]
end_time = arguments[3]

mail_subject = "AfterHours Invitation - {0}".format(invitation_subject)
common_mail.send_invitation_message("afterhour.mailservice@gmail.com", 'Rambam123456', to_list, mail_subject, start_time, end_time, mail_subject, '')