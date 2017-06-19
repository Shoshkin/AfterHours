import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText

me = "afterhour.mailservice@gmail.com"
you = "liad.mbs@gmail.com"

server = smtplib.SMTP('smtp.gmail.com', 587)
server.starttls()
server.login(me, "Rambam123456")



# Create message container - the correct MIME type is multipart/alternative.


# Send the message via local SMTP server.
s = smtplib.SMTP('smtp.gmail.com', 587)
# sendmail function takes 3 arguments: sender's address, recipient's address
# and message to send - here it is sent as one string.
server.sendmail(me, you, msg.as_string())
server.quit()