import mysql.connector


datos={
	
   'user':'root',
   'password':'1234',
   'database':'dbuser'
 }


cnx = mysql.connector.connect(**datos)
cursor = cnx.cursor()



add_user = ("INSERT INTO tbuser "
               "(nombre, apellido_p,apellido_m) "
               "VALUES (%s, %s, %s)")

print('Nombre:')
nombre=input()

print('Apellido paterno:')
ap_p=input()

print('Apellido materno:')
ap_m=input()


data_user = (nombre, ap_p,ap_m)

# Insert new employee
cursor.execute(add_user,data_user)

# Make sure data is committed to the database
cnx.commit()

print ('Registro exitoso')

cursor.close()

cnx.close()
