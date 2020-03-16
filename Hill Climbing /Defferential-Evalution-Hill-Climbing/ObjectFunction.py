#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Fri Feb 28 00:29:17 2020

@author: rayansami
"""
import math

# Object Function (Cost function)
def eggholderFunction(paramxy):
    x = paramxy[0]
    y = paramxy[1]
    left = math.sin(math.sqrt(math.fabs(x / 2 + (y + 47))))
    right = math.sin(math.sqrt(math.fabs(x - (y+ 47))))
    
    functValue = -(y+47) * left - x*right
    return functValue