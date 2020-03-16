#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Sun Feb 23 18:43:47 2020

@author: rayansami

Hints taken from https://github.com/dsubram1/NQueens/blob/master/HillClimbingRandomRestart.java
"""
import math
import numpy as np
import copy 


class NQueen:
    def __init__(self,row,column):
        self.row = row
        self.column = column
        
    def checkPairConflict(self,nq):
        # Check by row & column
        if self.row == nq.getRow() or self.column == nq.getColumn():
            return 1
        
        # Check diagonally        
        if math.fabs(self.row - nq.getRow()) == math.fabs(self.column - nq.getColumn()):            
            return 1
        
        return 0
    
    def moveQueen(self):
        self.row =  self.row + 1
            
        
    def getRow(self):
        return self.row
    
    def getColumn(self):
        return self.column
    
# Calculating huristics based on conflicting pair of Queens
def calculateHuristic(nQ_states):
    huristic = 0
    # Taking a queen in one column and checking it with other columns
    for i in range(0,len(nQ_states)):
            for j in range(i+1,len(nQ_states)):
                if nQ_states[i].checkPairConflict(nQ_states[j]):
                    huristic = huristic + 1
    
    return huristic
    
# Generate Queens for each column at a random row position
def generateBoard(n):
    board = [NQueen(np.random.randint(0,n),column) for column in range(0,n)]
    return board

def printBoard(nQBoard):
    boardLength = len(nQBoard)
    # Generate a n x n board of 0's
    board = np.zeros((boardLength,boardLength)).astype('int')
    # Put 1 on the queen positions
    for i in range(0,boardLength):        
        board[nQBoard[i].getRow(),nQBoard[i].getColumn()] = 1    
    
    print(np.matrix(board))
    
def operation(iteration,n):    
    currentBoard = generateBoard(n)
    currentHuristic = calculateHuristic(currentBoard)
    
    bestHuristic = currentHuristic
    
    solutionFound = 0 # bool    
    isAlreadyYield = 0 # bool
    
    # Printing initial stages n-queens for first 10 iterations
    if iteration < 10:
        print('For iteration',iteration+1,'of',n,' Queens')          
        print('Initial Board')
        printBoard(currentBoard)

    tempBoard = copy.deepcopy(currentBoard)# [currentBoard[qn] for qn in range(0,n)]
    
    stepCount = 0    
    # If last 100 step does not bring better solution, then stop the iteration
    while(stepCount < 100):
        # Travel through the columns
        for i in range(0,n):
            # Setting the Temporary Board to the original positions       
            if i>0:
                tempBoard[i-1] = NQueen(currentBoard[i-1].getRow(),currentBoard[i-1].getColumn())
            # Set the queen to the first row of the selected column
            tempBoard[i] = NQueen(0,tempBoard[i].getColumn())
            
            # Now put the queen on each row of ith column and calculate the board huristics
            for j in range(0,n):
                # Huristic of the queeen on jth row and ith column
                tempHuristic = calculateHuristic(tempBoard)
                # If the new huristic is lesser then update the current one
                if tempHuristic < currentHuristic:
                    currentHuristic = tempHuristic
                    # Check if huristic is 0. If it's 0 then we have found our solution for this iteration
                    if currentHuristic == 0:
                        currentBoard = copy.deepcopy(tempBoard)# [tempBoard[qn] for qn in range(0,n)]
                        solutionFound = 1 
                        isAlreadyYield = 1
                        yield solutionFound;
                        break
                    # Update the current board with the optimized queen settings
                    currentBoard = copy.deepcopy(tempBoard)#[tempBoard[qn] for qn in range(0,n)]
                
                # If the solution is not found after putting queens in a row of i-th column,
                # move the queen 1 row down on temporary board
                if (not solutionFound and (tempBoard[i].getRow() != n-1)):
                     tempBoard[i].moveQueen()
            
            # If solution is found we do not need to iterate other columns
            if(solutionFound):                
                break  
        
        # If solution is found we do not need to iterate through the steps
        if solutionFound:
            break
        
        # After going through the whole board, check if we found the better setting then before
        # Here bestHuristic is the one we started with
        if currentHuristic < bestHuristic:
            bestHuristic = currentHuristic
            stepCount = 0
        else:
            stepCount = stepCount + 1
        
                    
    if iteration < 10:          
        if(solutionFound):
            print('Final Solution Found')
            printBoard(currentBoard)
        else:
            print('Final Solution Not Found')
            printBoard(currentBoard)    

    #Only happens if solution not found.
    if(stepCount == 100 and not isAlreadyYield):
        yield 0
        

if __name__== "__main__":
  # For each n = 8,16 and 32, this program runs 100 times
  nValues = [8,16,32]      
  for n in nValues:
      sol = []
      for iteration in range(0,100):
          # Sending iteration count and number of queens each time
          sol = list(operation(iteration,n))
      solved = sol.count(1)
      print('Solution found for',n,'Queen is:',solved,'times')
      
      
