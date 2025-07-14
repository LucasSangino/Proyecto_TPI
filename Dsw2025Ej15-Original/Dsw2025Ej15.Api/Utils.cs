namespace Dsw2025Ej15.Api;


public interface ICalculadora
{
    int Sumar(int numero1, int numero2){
        return 0;
    }
}
public class Calculadora : ICalculadora
{
    int ICalculadora.Sumar(int numero1, int numero2){
        return numero1 + numero2;
    }
}
public class Test
{
    public void ProbarCalculadora(){
        ICalculadora calculadora = new Calculadora();
        var resultado = calculadora.Sumar(1, 2);
    }
}

