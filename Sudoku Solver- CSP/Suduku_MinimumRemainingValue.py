#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Fri Mar 13 12:12:14 2020

@author: rayansami
"""

'''
    For this Suduku board we have 3 constrains as followings:
        •	Any row contains more than one of the same number from 1 to 9 [#Case 1]
        •	Any column contains more than one of the same number from 1 to 9 [#Case 2]
        •	Any 3×3 grid contains more than one of the same number from 1 to 9 [#Case 3]
        
        NB: Each place in 9x9 suduku board is called square here

'''
import timeit

def isSimilarExistsInRow(arr,currentRow,value): #Case 1
    for i in range(len(arr)):
        if arr[currentRow][i] == value:
            return True
    return False

def isSimilarExistsInColumn(arr,currentColumn,value):  #Case 2
    for i in range(len(arr)):
        if arr[i][currentColumn] == value:
            return True
    return False
        
def isSimilarExistsInGrid(arr,row,col,value): # 3x3 Grid #Case 3
    for r in range(3):
        for c in range(3):
            if arr[r+row][c+col] == value:
                return True
    return False

'''
    Checking all the constrains together and return if true if the constrains are met
'''
def isNumberSafeToPlace(arr,currentRow,currentCol,value):
    if not isSimilarExistsInRow(arr,currentRow,value) and not isSimilarExistsInColumn(arr,currentCol,value) and not isSimilarExistsInGrid(arr,currentRow - currentRow%3,currentCol - currentCol%3,value):
        return True    
    return False

'''
    Check if grid is done by checking each square if it has value 0.
    Having value 0 means the square has yet to place a number
'''
def isGridFinished(arr):
    for r in range(len(arr)):
        for c in range(len(arr[1])):
            if arr[r][c] == 0:
                return False
    return True
'''
    Minimum Remaining Values:
    The next square to be assigned a number will be the square with the fewest legal numbers remaining.
'''
def findRemainingValuesForEmptyPosition(arr,row,column):
    for num in range(1,10):
        if isNumberSafeToPlace(arr,row,column,num):
            yield num

'''
    This method return the location of a square which has minimam legal moves(numbers this case) remaining
    Also yields a number from 1-9 if it passes the constrains   
'''
def findEmptyPositon(arr):
    currentLocation = (-1,-1) # Initializing the tuple with a value
    minValueSize = 9    # Initializin the minimum value with max-value 9 for 9x9 suduku
    remainingValues = [] # Initializing a list for remaining values
    for r in range(len(arr)):
        for c in range(len(arr[1])):
            if arr[r][c] == 0:
                checkedRemainingValues = list(findRemainingValuesForEmptyPosition(arr,r,c)) # List of remaining values for a certain square
                '''
                    Our target is to find a square which is the most constrained in the board and that has the minimum legal values left
                '''
                if minValueSize > len(checkedRemainingValues): # Check if the number of remaining value a lesser than previous
                    minValueSize = len(checkedRemainingValues) # If found better square 
                    remainingValues.clear() # Clearing the previous list
                    remainingValues = checkedRemainingValues[:] # Updating the list with newer values
                    currentLocation = (r,c) # Updating the new location
                                
    return currentLocation,remainingValues
        
def sudukuSolver(arr):
    currentLocation,remainingValues = findEmptyPositon(arr)
    currentRow = currentLocation[0]
    currentColumn = currentLocation[1]
    
    '''
        Check if Suduku is solved already
    '''
    if isGridFinished(arr):
        return True
    
    '''
        Go through each option[remaining number for current location]
    '''    
    for number in remainingValues:        
        arr[currentRow][currentColumn] = number 
        if sudukuSolver(arr):   # Recursive call          
            return True
        '''
            If the number does not work out then reset it with 0
        '''
        arr[currentRow][currentColumn] = 0
        
    return False    # Initiate backtrack
    
def main():
    '''
        Read the file

        easy: easyBoard
        medium: mediumBoard
        hard: hardBoard
        evil: evilBoard
    '''
    with open('evilBoard.txt','r') as f:
        x = f.read().splitlines() # splitlines() helps to avoid including newlines(\n)
    '''
        Next 3 lines can be written like this: [[int(row[i]) for i in range(len(row))] for row in x]    
        This goes to each line of x , iterates through the string and creates each integer value of a row
        After this we get a 2d 9x9 array
    '''
    sudukuBoard = [] 
    for row in x:        
        sudukuBoard.append([int(row[i]) for i in range(len(row))])        

    start = timeit.default_timer()
    if sudukuSolver(sudukuBoard):
        for r in range(len(sudukuBoard)):
            print(sudukuBoard[r])
    else:
        print('No solution')
            
    stop = timeit.default_timer()    
    print('Operation time required:',stop-start)

if __name__ == "__main__":
    main()