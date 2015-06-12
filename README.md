# NotificationSample
Como hemos visto en los post anteriores XAML tiene la capacidad de comprender expresiones de [**atado de datos**](https://saturninopimentel.com/mvvm-ii-trabajando-con-atado-de-datos/) y [**la vista**](https://saturninopimentel.com/mvvm-iii-la-vista/) juega un papel primordial en la interacción con el usuario, pues bien por lo general esa interacción entre el usuario y nuestra aplicación produce cambios en la información que contiene nuestra aplicación, estos cambios ya sea por procesos de nuestra  aplicación o por datos proporcionados por los usuario son manejados por medio de un sistema de notificación de cambios que permite a los componentes mantener el estado de la información actualizado según convenga.

La notificación de cambios es muy sencilla pero de igual manera importante, XAML permite tres tipos de comunicación una de ellas es **OneWay** que le permitirá a las propiedades informar a la vista de los cambios pero la propiedad no recibe información de los cambios que sufra en la vista.
```language-csharp
 <TextBox Text="{Binding Message, Mode=OneWay}" />
```
 **TwoWay** que permite la notificación de cambios de manera bidireccional, es decir nuestras propiedades serán capaces de enviar y recibir información de la vista.
```language
<TextBox Text="{Binding Name, Mode=TwoWay}" />
```
Por último tenemos la notificación **OneTime** que solo informará (como su nombre lo dice) una vez.
```language-csharp
 <TextBlock Text="{Binding Id,Mode=OneTime}" />
```
***Nota**:También existen otros dos métodos de notificación **OneWayToSource** y **Default** que están disponibles para WPF pero he mencionado los tres que son comunes en todas las platatormas*
####INotifyPropertyChanged
Para hacer uso del sistema de notificación de cambios solo tenemos que implementar la interfaz **INotifyPropertyChanged**, esta interfaz tiene un elemento el cual es un evento (sí un solo evento) el cual nos permite informar que existen cambios en los datos, veamos la implementación de esta interfaz en el siguiente ejemplo.

```language-csharp

public class Person : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

   }

```
Lo primero que hacemos es validar que el evento **PropertyChanged** tenga un método adjunto que se encargue de controlar el evento, dicho de forma simple que **PropertyChanged** no sea nulo y después lanzamos el evento pasando como argumentos el elemento que lanza el evento y después una instancia de **PropertyChangedEventArgs** indicando el nombre de la propiedad que ha sufrido un cambio (si envías una cadena vacía se actualizan todas las propiedades).
Pero hacer esto en todas las propiedades es algo innecesario (y aburrido) así que vamos a crear un método que nos ayude a hacernos la vida más simple, para lo cual haremos uso del atributo **[CallerMemberName]** que forma parte de los atributos [**caller info**](https://msdn.microsoft.com/en-us/library/hh534540.aspx) (nacen en la verión 5 de C#) con lo cual nuestra clase quedará como se muestra a continuación.

```language-csharp
public class Person : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
```
En este punto puedes optar por crear una clase base de la cual puedas heredar y que contenga la implementación de **INotifyPropertyChanged** además de método **RaisePropertyChanged**, en lo personal en mis desarrollos lo hago, así que aquí te dejo la clase.

```language-csharp
public class BindableBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
```

####ObservableCollection<T>
En el caso de las colecciones de datos **ObservableCollection** te ayuda a mantener notificaciones cuando agregas, remueves o refrescas los datos de tu colección sin necesidad de agregar código, con lo cual nos facilita el trabajo que se realiza con los controles de colecciones de datos.

He dejado [aquí](https://github.com/Satur01/NotificationSample) un pequeño ejemplo de un proyecto de Windows Phone para que puedas revisar el código y como podrás ver implementar notificaciones es muy sencillo así que no te compliques la vida :D. Saludos.
