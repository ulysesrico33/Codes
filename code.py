import mysql.connector

import os



datos={ 'user':'root',

            'password':'1234',

            'database':'dbuser'

          }





def insert_file(texto):

 cnx = mysql.connector.connect(**datos)

 cursor = cnx.cursor()

 news=texto

 query="INSERT INTO tb_new (news) VALUES ('"+news+"')"

 cursor.execute(query)

 cnx.commit()

 cursor.close()

 cnx.close()



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