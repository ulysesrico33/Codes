

"""
This is a comment to see if everything is alright

BOARD

  0 1 2
0 X X O
1 O X O
2 X O O

"""
#Global variables
x="X";
o="O";
board=[
           [x,o,o],
           [x,x,o],
           [o,x,o]
           ];

pos00=board[0][0];
pos01=board[0][1];
pos02=board[0][2];
center=board[1][1];
pos10=board[1][0];
pos12=board[1][2];
pos20=board[2][0];
pos21=board[2][1];
pos22=board[2][2];

def maingame(): 
    play(board);

def play(game):
	print("Welcome to the game");
	lsFinal1=checkVertical(x,game);
	lsFinal2=checkVertical(o,game);
	lsFinal3=checkFlat(x,game);
	lsFinal4=checkFlat(o,game);
	lsFinal5=checkDiagonal(o,game);
	lsFinal6=checkDiagonal(x,game);
	

	if lsFinal1[0]==True:
	    print("Winner is :"+lsFinal1[1])
	elif lsFinal2[0]==True:
	    print("Winner is :"+lsFinal2[1])
	elif lsFinal3[0]==True:
	    print("Winner is :"+lsFinal3[1])
	elif lsFinal4[0]==True:
	    print("Winner is :"+lsFinal4[1])
	elif lsFinal5[0]==True:
	    print("Winner is :"+lsFinal5[1])   
	elif lsFinal6[0]==True:
	    print("Winner is :"+lsFinal6[1])
	else:
	    print("Draw game!")         

	

def checkVertical(shape,board):
	bwin=False;
	if pos00==shape and pos10==shape and pos20==shape:
		bwin=True
	if pos01==shape and center==shape and pos21==shape:
		bwin=True
	if pos02==shape and pos12==shape and pos22==shape:
		bwin=True
	lsscore=[];
	lsscore=[bwin,shape];	
	return	lsscore


def checkFlat(shape,board):
    bwin=False;
    if pos00==shape and pos01==shape and pos02==shape:
	    bwin=True
    if pos10==shape and center==shape and pos12==shape:
	    bwin=True
    if pos20==shape and pos21==shape and pos22==shape:
	    bwin=True
    lsscore=[];
    lsscore=[bwin,shape];		
    return lsscore

def checkDiagonal(shape,board):
    bwin=False;
    if pos00==shape and center==shape and pos22==shape:
        bwin=True;
    if pos20==shape and center==shape and pos02==shape:
        bwin=True; 
    lsscore=[];
    lsscore=[bwin,shape];		
    return lsscore           



if __name__ == '__main__':
    maingame()





