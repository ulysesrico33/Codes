List numbers= new ArrayList();

        List numbersAdded= new ArrayList();

        Scanner input_number = new Scanner(System.in);

        Scanner input_response = new Scanner(System.in);

        String res;

        Map<Integer, Integer> hm = new HashMap<Integer, Integer>();



        do{

       

            System.out.println("Agrega un número a la lista:");

            numbers.add(input_number.nextInt());

            System.out.println("¿Deseas agregar otro número? (S/N ): ");

            res=input_response.nextLine().toUpperCase();

         

           

       

        }while(res.equals("S"));

       

       

        Iterator iterator = numbers.iterator();

        while(iterator.hasNext()) {

         

            //Si el elemento existe, entonce aumentar en 1

            int elemento=Integer.parseInt(iterator.next().toString());

           if(hm.containsKey(elemento)){   

       

               int ocurrencia=Integer.parseInt(hm.get(elemento).toString())+1;

               hm.replace(elemento,ocurrencia);

      }else{

             

               hm.put(elemento, 1);

             

           }



    }

       

        //Analizo cada entrada para saber el primer elemento repetido

        boolean salir=false;

        for(Map.Entry<Integer,Integer> numero:hm.entrySet()){

           

            if(!salir){

                if(numero.getValue()>1){

           

                System.out.println("El número "+numero.getKey()+" es el primer elemento repetido");

                salir=true;

               

               }

            }

       

        }