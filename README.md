# High Low algorithm - NetCore C#

## Definición

El algoritmo hi/lo es una estrategia para generar el identificador secuencial de una tabla de base de datos.

En vez de generar un número secuencial cada vez que se crea un registro, la aplicación cliente reserva un bloque de identificadores. De esta manera, cada vez que requiere insertar un registro, ya no será necesario que la base de datos genere el identificador, y se ahorrarán llamadas a la base de datos.

El valor "hi" (high) es inicializado y controlado a nivel de base de datos.

El valor "maxLo" (low) se define a nivel de aplicación, y es el tamaño del bloque de identificadores que la aplicación puede reservar.

El valor "lo" es el identificador a ser usado por la aplicación cliente. Por tanto, el cliente lleva control del valor "lo".

El rango de identificadores disponibles está dado por la siguiente fórmula:

start=(maxLo+1)*hi,end=start+maxLo

Cuando el bloque es usado, se obtiene un nuevo valor "hi" de la base de datos.

## Ejemplo

En el caso de esta API REST, tenemos lo siguiente:

El valor "hi" inicializado en "1" en la tabla "ids" para la tabla "customer"

El valor "maxLo" está configurado en "9" en EnterprisePatterns.Api.Common.Infrastructure.Persistence.NHibernate.HiLoConvention

Si varios clientes web se conectan a la API REST, cada uno reservará un bloque de identificadores.

El cliente 1 reservaría el bloque del 10 al 19, y el cliente 2 del 20 al 29.

De esta forma, el cliente 2 puede insertar un registro con identificador 23 antes que el cliente 1 inserte el registro con identificador 15.

## Documentación API REST

Crea "n" customers concatenando el OrganizationName + el número
```
POST /v1/customers/hilo?n=10
{
	"OrganizationName": "ABC Technologies"
}
```

Crea un solo customer
```
POST /v1/customers/
{
	"OrganizationName": "ABC Technologies"
}
```

Obtiene la lista de customers
```
GET /v1/customers/
[
    {
        "id": 10,
        "organizationName": "Chapopote 1"
    },
    {
        "id": 11,
        "organizationName": "Chapopote 2"
    }
]
```