#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Tue Feb 25 08:16:30 2020

@author: rayansami
"""
import numpy as np
import matplotlib.pyplot as plt
import timeit
from ObjectFunction import eggholderFunction

def differentialEvalution(eggholderFunction, bounds, mutationFactor=1, crossoverRate=0.7, populationSize = 20):
    dimensions = len(bounds)
    
    #using normalized values for calculative advantage
    randomPopulation_normalized = np.random.rand(populationSize, dimensions) 
    min_b, max_b = np.asarray(bounds).T
    diff = np.fabs(min_b - max_b)
    randomPopulation = min_b + randomPopulation_normalized * diff
    
    #calculate the fitness for each individual in random population    
    fitness = np.asarray([eggholderFunction(ind) for ind in randomPopulation])
    fittestIndividualPosition = np.argmin(fitness) # check the address of fittest(minimum) value
    fittestIndividual = randomPopulation[fittestIndividualPosition]
    
    iterationCount = 0    
    # This iteration will be stoppped if last 100 generations won't produce any better value
    while iterationCount < 100:        
        # Cycle through the each individual of Current Generation
        for j in range(populationSize): 
            ## -------- -------- Mutation -------- --------##

            # Select three random vector index population and ignore the current vector j
            indvIds = [index for index in range(populationSize) if index != j]
            individual1, individual2, individual3 = randomPopulation_normalized[np.random.choice(indvIds, 3, replace = False)]
            targetIndividual = randomPopulation_normalized[j]
            
            # Get the Donor vector
            donor = np.clip(individual1 + mutationFactor * (individual2 - individual3), 0, 1) # This is normalized donor value. Cliping helps to maintain the bound
            
            ## -------- -------- Crossover (Recombination) -------- --------##
            """"
             If this random value is less than the recombination rate, recombination 
             occurs and we swap out the current variable in our target vector with the 
             corresponding variable in the donor vector. 
             
             If the randomly generated value is
             greater than the recombination rate, recombination does not happen and the variable 
             in the target vector is left alone. 
             
             This new offspring individual is called the trial vector.
            """
            crossover = np.random.rand(dimensions) < crossoverRate
            trial_normalized = np.where(crossover, donor, targetIndividual)
            trial = min_b + trial_normalized * diff

            ## -------- -------- Selection -------- --------## trialScore
            trialScore = eggholderFunction(trial)

            if trialScore < fitness[j]: # fitness[j] is the targetScore
                fitness[j] = trialScore
                randomPopulation_normalized[j] = trial_normalized
                
            # Check which value is the lowest between trial & current fittest. yeild the minimum
            if trialScore < fitness[fittestIndividualPosition]:
                iterationCount = 0
                # Change the fittest position to j
                fittestIndividualPosition = j
                fittestIndividual = trial
            else:
                iterationCount = iterationCount + 1
            
            if iterationCount > 100:
                break
            
        yield fittestIndividual, fitness[fittestIndividualPosition]
        

bounds = [(-10000,10000),(-10000,10000)] # =10000 < x,y < 10000
minima = []
start = timeit.default_timer()
for run in range(0,100):
    deValues = list(differentialEvalution(eggholderFunction,bounds))
    # Last Entry of the list contains the minima
    lastEntry = deValues[-1] 
    # Get the minumul Object Function for each run
    minima.append(lastEntry[1])
stop = timeit.default_timer()    

plt.plot(range(0,100),minima)
plt.xlabel('Iteration')
plt.ylabel('Egg Function Value')
plt.title('Egg Function Vs Iteration')
plt.show()    
print('Operation time required:',stop-start)