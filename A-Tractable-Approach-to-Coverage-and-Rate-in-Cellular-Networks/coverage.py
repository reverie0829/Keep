import numpy as np
import scipy.integrate as integrate
import matplotlib.pyplot as plt
from tqdm import tqdm

#參數設定
lamb = 1   
alpha = 4
sigma = 1
u = 1



def Laplace( u, T, r  ):
    def first_integral(g):
        first_integral = lambda v: 1 - np.exp((-u * T * r**alpha * v**-alpha * g) * v )
        first_val , err = integrate.quad(first_integral, r, np.Infinity)
        return first_val
    
    second_integral = lambda g: (first_integral(g) * lamb * np.exp(-lamb * g))
    second_val , err = integrate.quad(second_integral, 0, np.Infinity)
    Laplace_result = np.exp(-2 * np.pi * lamb * second_val)
    return Laplace_result
    
def Coverage( T, lamb, alpha ) :
    Pc = lambda r : np.exp( (-1)*np.pi*lamb*r**2 )*np.exp( (-1)*u*T*r**alpha*sigma**2 )*2*np.pi*lamb*r*Laplace( u, T, r )
    result ,err = integrate.quad(Pc, 0, np.Infinity)
    return result


if __name__ == "__main__":
    T_range = np.linspace( -10, 20, 50 )
    Log_range = 10**(T_range/10)
    C = list()
    with tqdm(total=len(Log_range), ascii=True) as pbar:
        for T in Log_range:
            C.append( Coverage( T, lamb, alpha ) )
            pbar.update(1)
    print(C)
    plt.plot( T_range, C )
    plt.yticks(np.arange(0,1,0.1))
    plt.show()