import smtplib
from email.MIMEMultipart import MIMEMultipart
from email.MIMEBase import MIMEBase
from email.MIMEText import MIMEText
from email.Utils import COMMASPACE, formatdate
from email import Encoders
import os,datetime

CRLF = "\r\n"
login =  "afterhour.mailservice@gmail.com"
password = "Rambam123456"
attendees = ["liad.mbs@gmail.com"]
organizer = "ORGANIZER;CN=organiser:mailto:first"+CRLF+" @gmail.com"
fro = "AfterHour <afterhour.mailservice@gmail.com>"

ddtstart = datetime.datetime.now()
dtoff = datetime.timedelta(days = 1)
dur = datetime.timedelta(hours = 1)
ddtstart = ddtstart +dtoff
dtend = ddtstart + dur
dtstamp = datetime.datetime.now().strftime("%Y%m%dT%H%M%SZ")
dtstart = ddtstart.strftime("%Y%m%dT%H%M%SZ")
dtend = dtend.strftime("%Y%m%dT%H%M%SZ")

description = "DESCRIPTION: test invitation from pyICSParser"+CRLF
attendee = ""
for att in attendees:
    attendee += "ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-    PARTICIPANT;PARTSTAT=ACCEPTED;RSVP=TRUE"+CRLF+" ;CN="+att+";X-NUM-GUESTS=0:"+CRLF+" mailto:"+att+CRLF
ical = "BEGIN:VCALENDAR"+CRLF+"PRODID:pyICSParser"+CRLF+"VERSION:2.0"+CRLF+"CALSCALE:GREGORIAN"+CRLF
ical+="METHOD:REQUEST"+CRLF+"BEGIN:VEVENT"+CRLF+"DTSTART:"+dtstart+CRLF+"DTEND:"+dtend+CRLF+"DTSTAMP:"+dtstamp+CRLF+organizer+CRLF
ical+= "UID:FIXMEUID"+dtstamp+CRLF
ical+= attendee+"CREATED:"+dtstamp+CRLF+description+"LAST-MODIFIED:"+dtstamp+CRLF+"LOCATION:"+CRLF+"SEQUENCE:0"+CRLF+"STATUS:CONFIRMED"+CRLF
ical+= "SUMMARY:test "+ddtstart.strftime("%Y%m%d @ %H:%M")+CRLF+"TRANSP:OPAQUE"+CRLF+"END:VEVENT"+CRLF+"END:VCALENDAR"+CRLF

eml_body = "Email body visible in the invite of outlook and outlook.com but not google calendar"
msg = MIMEMultipart('mixed')
msg['Reply-To']=fro
msg['Date'] = formatdate(localtime=True)
msg['Subject'] = "pyICSParser invite"+dtstart
msg['From'] = fro
msg['To'] = ",".join(attendees)

part_email = MIMEText(eml_body,"html")
part_cal = MIMEText(ical,'calendar;method=REQUEST')

msgAlternative = MIMEMultipart('alternative')
msg.attach(msgAlternative)

ical_atch = MIMEBase('application/ics',' ;name="%s"'%("invite.ics"))
ical_atch.set_payload(ical)
Encoders.encode_base64(ical_atch)
ical_atch.add_header('Content-Disposition', 'attachment; filename="%s"'%("invite.ics"))

eml_atch = MIMEBase('text/plain','')
Encoders.encode_base64(eml_atch)
eml_atch.add_header('Content-Transfer-Encoding', "")

msgAlternative.attach(part_email)
msgAlternative.attach(part_cal)

mailServer = smtplib.SMTP('smtp.gmail.com', 587)
mailServer.login(login, password)
mailServer.sendmail(fro, attendees, msg.as_string())
mailServer.close()