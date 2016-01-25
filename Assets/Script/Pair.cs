using UnityEngine;
using System.Collections;


public class Pair<T, U> {
    private T first;
    private U second;

    public Pair(T fst, U scnd)
    {
        first = fst;
        second = scnd;
    }

    public T getFirst()
    {
        return first;
    }

    public U getSecond()
    {
        return second;
    }

    public void setFirst(T f)
    {
        first = f;
    }

    public void setSecond(U s)
    {
        second = s;
    }
}
