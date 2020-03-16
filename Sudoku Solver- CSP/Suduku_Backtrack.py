# -*- coding: utf-8 -*-
"""
Created on Wed Mar 11 14:34:28 2020

@author: muddi
"""

'''
    For this Suduku board we have 3 constrains as followings:
        •	Any row contains more than one of the same number from 1 to 9 [#Case 1]
        •	Any column contains more than one of the same number from 1 to 9 [#Case 2]
        •	Any 3×3 grid contains more than one of the same number from 1 to 9 [#Case 3]

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

def isNumberSafeToPlace(arr,currentRow,currentCol,value):
    if not isSimilarExistsInRow(arr,currentRow,value) and not isSimilarExistsInColumn(arr,currentCol,value) and not isSimilarExistsInGrid(arr,currentRow - currentRow%3,currentCol - currentCol%3,value):
        return True    
    return False

def isGridFinished(arr):
    for r in range(len(arr)):
        for c in range(len(arr)):
            if arr[r][c] == 0:
                return False
    return True

def findEmptyPositon(arr):
    currentLocation = (-1,-1)   # Initializing the tuple with a value
    for r in range(len(arr)):
        for c in range(len(arr[1])):
            if arr[r][c] == 0:
                currentLocation = (r,c)
                return currentLocation
    return currentLocation
        
def sudukuSolver(arr):
    currentLocation = findEmptyPositon(arr)
    currentRow = currentLocation[0]
    currentColumn = currentLocation[1]
    
    '''
        Check if Suduku is solved already
    '''
    if isGridFinished(arr):
        return True
        
    '''
        Go through each option(number 1 to 9)
    '''    
    for number in range(1,10):        
        if isNumberSafeToPlace(arr,currentRow,currentColumn,number): # Check constrains for this number
            arr[currentRow][currentColumn] = number                       
            if sudukuSolver(arr):   # Recursive call
                return True
            '''
                If the number does not work out then reset it with 0
            '''
            arr[currentRow][currentColumn] = 0
            
    return False  # Initiate backtrack
        
    
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