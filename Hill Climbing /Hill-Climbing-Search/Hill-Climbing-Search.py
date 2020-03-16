#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Fri Feb 21 21:16:38 2020

@author: rayansami
"""

import math
import numpy as np
import matplotlib.pyplot as plt
import timeit

# Object Function
def eggholderFunction(x, y):
    left = math.sin(math.sqrt(math.fabs(x / 2 + (y + 47))))
    right = math.sin(math.sqrt(math.fabs(x - (y+ 47))))
    
    functValue = -(y+47) * left - x*right
    return functValue

# Generate a random value
def getRandomValue():
    return np.random.uniform(-10000,10000)

def hillClimbingSearch(initialX,initialY):
     currentX = initialX
     currentY = initialY
     
     currentObjectValue = eggholderFunction(currentX,currentY)
     stepCount = 0
     
     while(stepCount != 100):
         newX = (getRandomValue() - 0.5) * 1.0 + currentX
         newY = (getRandomValue() - 0.5) * 1.0 + currentY
         newObjectValue = eggholderFunction(newX,newY)
         #print('step:',stepCount)
         if(newObjectValue < currentObjectValue):
             currentX = newX
             currentY = newY
             currentObjectValue = newObjectValue
             #print('\nMinima:', currentObjectValue)
             stepCount = 0
             
         else:
             stepCount = stepCount + 1        
             
             
     #print('Found the global minimum. It is:', currentObjectValue)
     return currentObjectValue
     
     

map = []   
start = timeit.default_timer()
for i in range(0,100):
    map.append(hillClimbingSearch(getRandomValue(),getRandomValue()))
stop = timeit.default_timer()    

#for i in range(0,100):
plt.plot(range(0,100),map)
plt.xlabel('Iteration')
plt.ylabel('Egg Function Value')
plt.title('Egg Function Vs Iteration')
plt.show()    
print('Operation time required:',stop-start)

         
    
