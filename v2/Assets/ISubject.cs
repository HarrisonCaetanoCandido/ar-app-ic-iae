// Uso esta interface para as APIs necessárias no projeto
public interface ISubject
{
    public void RegisterObserver(IObserver o);
    public void RemoveObserver(IObserver o);
    public void NotifyObservers();
}
