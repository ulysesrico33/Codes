import mysql.connector
import os

datos={ 'user':'root',
            'password':'1234',
            'database':'dbuser'
          }

#Variables globales
query=''
def seleccion():
	cnx = mysql.connector.connect(**datos) 
	cursor = cnx.cursor()
	query="select * from tb_new"
	cursor.execute(query)
	campos=cursor.fetchall()
	for campo in campos:
		c_id=str(campo[0])
		noticia=campo[1]
		print('Id: '+c_id+'')
		print('Noticia:'+noticia+'')
	cursor.close()
	cnx.close()


def insert_file(texto):
	cnx = mysql.connector.connect(**datos) 
	cursor = cnx.cursor()
	news=texto
	query="insert into tb_new (news) values ('"+news+"')"
	cursor.execute(query)
	cnx.commit()
	cursor.close()
	cnx.close()

print("Elige una opción")
print()
print("1. Insertar archivo")
print("2. Selección")
res=input()
if res==1:
	directory='./doc/'
	ls=os.listdir(directory)
	for file in ls:
		if file.endswith(".txt"):
			archivo = open(directory+file,'r')
			texto=''
			for line in archivo:
				texto+=line
			insert_file(texto)	
			archivo.close()
			print ('Registro exitoso') 
else:
	seleccion()