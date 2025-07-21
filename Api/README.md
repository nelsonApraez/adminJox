
# Contributor Code of Conduct

## Our Pledge

In the interest of fostering an open and welcoming environment, we as
contributors and maintainers pledge to making participation in our project and
our community a harassment-free experience for everyone, regardless of age, body
size, disability, ethnicity, sex characteristics, gender identity and expression,
level of experience, education, socio-economic status, nationality, personal
appearance, race, religion, or sexual identity and orientation.

## Our Standards

Examples of behavior that contributes to creating a positive environment
include:

* Using welcoming and inclusive language
* Being respectful of differing viewpoints and experiences
* Gracefully accepting constructive criticism
* Focusing on what is best for the community
* Showing empathy towards other community members

Examples of unacceptable behavior by participants include:

* The use of sexualized language or imagery and unwelcome sexual attention or
 advances
* Trolling, insulting/derogatory comments, and personal or political attacks
* Public or private harassment
* Publishing others' private information, such as a physical or electronic
 address, without explicit permission
* Other conduct which could reasonably be considered inappropriate in a
 professional setting

## Our Responsibilities

Project maintainers are responsible for clarifying the standards of acceptable
behavior and are expected to take appropriate and fair corrective action in
response to any instances of unacceptable behavior.

Project maintainers have the right and responsibility to remove, edit, or
reject comments, commits, code, wiki edits, issues, and other contributions
that are not aligned to this Code of Conduct, or to ban temporarily or
permanently any contributor for other behaviors that they deem inappropriate,
threatening, offensive, or harmful.

## Enforcement

Instances of abusive, harassing, or otherwise unacceptable behavior may be
reported by contacting the project team at admin@outlook.com. All
complaints will be reviewed and investigated and will result in a response that
is deemed necessary and appropriate to the circumstances. The project team is
obligated to maintain confidentiality with regard to the reporter of an incident.
Further details of specific enforcement policies may be posted separately.

Project maintainers who do not follow or enforce the Code of Conduct in good
faith may face temporary or permanent repercussions as determined by other
members of the project's leadership.


# Swagger
- http2://localhost:5002/swagger/index.html


# Define operations in terms of HTTP methods
The HTTP protocol defines a number of methods that assign semantic meaning to a request. The common HTTP methods used by most RESTful web APIs are:

- GET retrieves a representation of the resource at the specified URI. The body of the response message contains the details of the requested resource.
- POST creates a new resource at the specified URI. The body of the request message provides the details of the new resource. Note that POST can also be used to trigger operations that don't actually create resources.
- PUT either creates or replaces the resource at the specified URI. The body of the request message specifies the resource to be created or updated.
- PATCH performs a partial update of a resource. The request body specifies the set of changes to apply to the resource.
- DELETE removes the resource at the specified URI.

# best practices POST GET PUT DELETE
/customers	Create a new customer	Retrieve all customers	Bulk update of customers	Remove all customers
/customers/1	Error	Retrieve the details for customer 1	Update the details of customer 1 if it exists	Remove customer 1
/customers/1/orders	Create a new order for customer 1	Retrieve all orders for customer 1

#Filtring data
/orders?limit=25&offset=50

# Versioning
# URI versioning  https://todo-works.com/v2/customers/3

- produces

HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
{"id":3,"name":"Contoso LLC","dateCreated":"2014-09-04T12:11:38.0376089Z","address":{"streetAddress":"1 Microsoft Way","city":"Redmond","state":"WA","zipCode":98053}}

# Query string versioning
# https://Todo-works.com/customers/3?version=2 produces

# Header versioning

GET https://Todo-works.com/customers/3 HTTP/1.1
Custom-Header: api-version=1

HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
{"id":3,"name":"Contoso LLC","address":"1 Microsoft Way Redmond WA 98053"}


# Media type versioning

GET https://Todo-works.com/customers/3 HTTP/1.1
Accept: application/vnd.Todo-works.v1+json

HTTP/1.1 200 OK
Content-Type: application/vnd.Todo-works.v1+json; charset=utf-8
{"id":3,"name":"Contoso LLC","address":"1 Microsoft Way Redmond WA 98053"}

#Common error HTTP status codes include:

# Respuestas informativas
100 Continue
Esta respuesta provisional indica que todo hasta ahora está bien y que el cliente debe continuar con la solicitud o ignorarla si ya está terminada.
101 Switching Protocol
Este código se envía en respuesta a un encabezado de solicitud Upgrade por el cliente e indica que el servidor acepta el cambio de protocolo propuesto por el agente de usuario.
102 Processing (WebDAV)
Este código indica que el servidor ha recibido la solicitud y aún se encuentra procesandola, por lo que no hay respuesta disponible.

#Respuestas satisfactorias
200 OK
La solicitud ha tenido éxito. El significado de un éxito varía dependiendo del método HTTP:
GET: El recurso se ha obtenido y se transmite en el cuerpo del mensaje.
HEAD: Los encabezados de entidad están en el cuerpo del mensaje.
PUT o POST: El recurso que describe el resultado de la acción se transmite en el cuerpo del mensaje.
TRACE: El cuerpo del mensaje contiene el mensaje de solicitud recibido por el servidor.
201 Created
La solicitud ha tenido éxito y se ha creado un nuevo recurso como resultado de ello. Ésta es típicamente la respuesta enviada después de una petición PUT.
202 Accepted
La solicitud se ha recibido, pero aún no se ha actuado. Es una petición "Sin compromiso", lo que significa que no hay manera en HTTP que permita enviar una respuesta asíncrona que indique el resultado del procesamiento de la solicitud. Está pensado para los casos en que otro proceso o servidor maneja la solicitud, o para el procesamiento por lotes.
203 Non-Authoritative Information
La petición se ha completado con éxito, pero su contenido no se ha obtenido de la fuente originalmente solicitada, sino que se recoge de una copia local o de un tercero. Excepto esta condición, se debe preferir una respuesta de 200 OK en lugar de esta respuesta.
204 No Content
La petición se ha completado con éxito pero su respuesta no tiene ningún contenido, aunque los encabezados pueden ser útiles. El agente de usuario puede actualizar sus encabezados en caché para este recurso con los nuevos valores.
205 Reset Content
La petición se ha completado con éxito, pero su respuesta no tiene contenidos y además, el agente de usuario tiene que inicializar la página desde la que se realizó la petición, este código es útil por ejemplo para páginas con formularios cuyo contenido debe borrarse después de que el usuario lo envíe.
206 Partial Content
La petición servirá parcialmente el contenido solicitado. Esta característica es utilizada por herramientas de descarga como wget para continuar la transferencia de descargas anteriormente interrumpidas, o para dividir una descarga y procesar las partes simultáneamente.
207 Multi-Status (WebDAV)
Una respuesta Multi-Estado transmite información sobre varios recursos en situaciones en las que varios códigos de estado podrían ser apropiados. El cuerpo de la petición es un mensaje XML.
208 Multi-Status (WebDAV)
El listado de elementos DAV ya se notificó previamente, por lo que no se van a volver a listar.
226 IM Used (HTTP Delta encoding)
El servidor ha cumplido una petición GET para el recurso y la respuesta es una representación del resultado de una o más manipulaciones de instancia aplicadas a la instancia actual.

#Redirecciones
300 Multiple Choice
Esta solicitud tiene más de una posible respuesta. User-Agent o el usuario debe escoger uno de ellos. No hay forma estandarizado de seleccionar una de las respuestas.
301 Moved Permanently
Este código de respuesta significa que la URI  del recurso solicitado ha sido cambiado. Probablemente una nueva URI sea devuelta en la respuesta.
302 Found
Este código de respuesta significa que el recurso de la URI solicitada ha sido cambiado temporalmente. Nuevos cambios en la URI serán agregados en el futuro. Por lo tanto, la misma URI debe ser usada por el cliente en futuras solicitudes.
 
303 See Other
El servidor envia esta respuesta para dirigir al cliente a un nuevo recurso solcitado a otra dirección usando una petición GET.
304 Not Modified
Esta es usada para propositos de "caché". Le indica al cliente que la respuesta no ha sido modificada. Entonces, el cliente puede continuar usando la misma versión almacenada en su caché.
305 Use Proxy 
Fue definida en una versión previa de la especificación del protocolo HTTP para indicar que una respuesta solicitada debe ser accedida desde un proxy. Ha quedado obsoleta debido a preocupaciones de seguridad correspondientes a la configuración de un proxy.
306 unused
 
Este código de respuesta ya no es usado más. Actualmente se encuentra reservado. Fue usado en previas versiones de la especificación HTTP1.1.
307 Temporary Redirect
El servidor envía esta respuesta para dirigir al cliente a obtener el recurso solicitado a otra URI con el mismo metodo que se uso la petición anterior. Tiene la misma semántica que el código de respuesta HTTP 302 Found, con la excepción de que el agente usuario no debe cambiar el método HTTP usado: si un POST fue usado en la primera petición, otro POST debe ser usado en la segunda petición.
308 Permanent Redirect
Significa que el recurso ahora se encuentra permanentemente en otra URI, especificada por la respuesta de encabezado HTTP Location:. Tiene la misma semántica que el código de respuesta HTTP 301 Moved Permanently, con la excepción de que el agente usuario no debe cambiar el método HTTP usado: si un POST fue usado en la primera petición, otro POST debe ser usado en la segunda petición.

#Errores de cliente
400 Bad Request
Esta respuesta significa que el servidor no pudo interpretar la solicitud dada una sintaxis inválida.
401 Unauthorized
Es necesario autenticar para obtener la respuesta solicitada. Esta es similar a 403, pero en este caso, autenticación es posible.
402 Payment Required
Este código de respuesta está reservado para futuros usos. El objetivo inicial de crear este código fue para ser utilizado en sistemas digitales de pagos. Sin embargo, no está siendo usado actualmente.
403 Forbidden
El cliente no posee los permisos necesarios para cierto contenido, por lo que el servidor está rechazando otorgar una respuesta apropiada.
404 Not Found
El servidor no pudo encontrar el contenido solicitado. Este código de respuesta es uno de los más famosos dada su alta ocurrencia en la web.
405 Method Not Allowed
El método solicitado es conocido por el servidor pero ha sido deshabilitado y no puede ser utilizado. Los dos métodos obligatorios, GET y HEAD, nunca deben ser deshabilitados y no debiesen retornar este código de error.
406 Not Acceptable
Esta respuesta es enviada cuando el servidor, despues de aplicar una negociación de contenido servidor-impulsado, no encuentra ningún contenido seguido por la criteria dada por el usuario.
407 Proxy Authentication Required
Esto es similar al código 401, pero la autenticación debe estar hecha a partir de un proxy.
408 Request Timeout
Esta respuesta es enviada en una conexión inactiva en algunos servidores, incluso sin alguna petición previa por el cliente. Significa que el servidor quiere desconectar esta conexión sin usar. Esta respuesta es muy usada desde algunos navegadores, como Chrome, Firefox 27+, o IE9, usa mecanismos de pre-conexión HTTP para acelerar la navegación. También hay que tener cuenta que algunos servidores simplemente desconectan la conexión sin enviar este mensaje.
409 Conflict
Esta respuesta puede ser enviada cuando una petición tiene conflicto con el estado actual del servidor.
410 Gone
Esta respuesta puede ser enviada cuando el contenido solicitado ha sido borrado del servidor.
411 Length Required
El servidor rechaza la petición porque el campo de encabezado Content-Length no esta definido y el servidor lo requiere.
412 Precondition Failed
El cliente ha indicado pre-condiciones en sus encabezados la cual el servidor no cumple.
413 Payload Too Large
La entidad de petición es más larga que los limites definidos por el servidor; el servidor puede cerrar la conexión o retornar un campo de encabezado Retry-After.
414 URI Too Long
La URI solicitada por el cliente es más larga que el servidor está dispuesto a interpretar.
415 Unsupported Media Type
El formato multimedia de los datos solicitados no está soportada por el servidor, por lo cual el servidor rechaza la solicitud.
416 Requested Range Not Satisfiable
El rango especificado por el campo de encabezado Range en la solicitud no cumple; es posible que el rango está fuera del tamaño de los datos objetivo del URI.
417 Expectation Failed
Significa que la expectativa indicada por el campo de encabezado Expect solicitada no puede ser cumplida por el servidor.
418 I'm a teapot
El servidor se reúsa a intentar hacer café con una tetera.
421 Misdirected Request
La petición fue dirigida a un servidor que no es capaz de producir una respuesta. Esto puede ser enviado por un servidor que no esta configurado para producir respuestas por la combinación del esquema y la autoridad que estan incluidos en la URI solicitada
422 Unprocessable Entity (WebDAV)
La petición estaba bien formada pero no se pudo seguir debido a errores de semántica.
423 Locked (WebDAV)
El recurso que está siendo accedido está bloqueado.
424 Failed Dependency (WebDAV)
La petición falló debido a una falla de una petición previa.
426 Upgrade Required
El servidor se reúsa a aplicar la solicitud usando el protocolo actual pero puede estar dispuesto a hacerlo después que el cliente se actualize a un protocolo diferente. El servidor envía un encabezado Upgrade en una respuesta para indicar los protocolos requeridos.
428 Precondition Required
El servidor origen requiere que la solicitud sea condicional. Tiene la intención de prevenir problemas de 'actualización perdida', donde un cliente OBTIENE un estado del recurso, lo modifica, y lo PONE devuelta al servidor, cuando mientras un tercero ha modificado el estado del servidor, llevando a un conflicto.
429 Too Many Requests
El usuario ha enviado demasiadas solicitudes en un periodo de tiempo dado.
431 Request Header Fields Too Large
El servidor no está dispuesto a procesar la solicitud porque los campos de encabezado son demasiado largos. La solicitud PUEDE volver a subirse después de reducir el tamaño de los campos de encabezado solicitados.
451 Unavailable For Legal Reasons
El usuario solicita un recurso ilegal, como alguna página web censurada por algún gobierno.

# Errores de servidor
500 Internal Server Error
El servidor ha encontrado una situación que no sabe como manejarla.
501 Not Implemented
El método solicitado no esta soportado por el servidor y no puede ser manejada. Los unicos métodos que los servidores requieren soporte (y por lo tanto no deben retornar este código) son GET y HEAD.
502 Bad Gateway
Esta respuesta de error significa que el servidor, mientras trabaja como una puerta de enlace para obtener una respuesta necesaria para manejar la petición, obtuvo una respuesta inválida.
503 Service Unavailable
El servidor no esta listo para manejar la petición. Causas comunes puede ser que el servidor está caido por mantenimiento o está sobrecargado. Hay que tomar en cuenta que junto con esta respuesta, una página usuario-amigable explicando el problema debe ser enviada. Estas respuestas deben ser usadas para condiciones temporales y el encabezado HTTP Retry-After: debería, si es posible, contener el tiempo estimado antes de la recuperación del servicio. El webmaster debe también cuidar los encabezados relacionados al caché que son enviados junto a esta respuesta, ya que estas respuestas de condicion temporal deben usualmente no estar en el caché.
504 Gateway Timeout
Esta respuesta de error es dada cuando el servidor está actuando como una puerta de enlace y no puede obtener una respuesta a tiempo.
505 HTTP Version Not Supported
La versión de HTTP usada en la petición no está soportada por el servidor.
506 Variant Also Negotiates
El servidor tiene un error de configuración interna: negociación de contenido transparente para la petición resulta en una referencia circular.
507 Insufficient Storage
El servidor tiene un error de configuración interna: la variable de recurso escogida esta configurada para acoplar la negociación de contenido transparente misma, y no es por lo tanto un punto final adecuado para el proceso de negociación.
508 Loop Detected (WebDAV)
El servidor detectó un ciclo infinito mientras procesaba la solicitud.
510 Not Extended
Extensiones adicionales para la solicitud son requeridas para que el servidor las cumpla.
511 Network Authentication Required
El código de estado 511 indica que el cliente necesita auntenticar para ganar acceso a la red.


## Convencion de nombres Prosperidad

A nivel de desarrollo de la aplicación se deben tener en cuenta como mínimo los siguientes aspectos:
1.    Se deben validar todas las entradas de información sobre el sistema, tipos de datos, rango de valores, obligatoriedad de información, manejo de caracteres especiales.
2.     Se deben controlar las operaciones complejas que puedan implicar un desbordamiento de memoria.
3.    Dentro de la aplicación no se debe implementar el uso de cookies para mantener información de datos sensibles del sistema.
4.     El sistema debe evitar la posibilidad de realizar inyección SQL que permita realizar consultas o modificaciones a nivel de base de datos.
5.    El sistema debe evitar la posibilidad realizar inyección de código HTML sobre el sistema de información, el cual pretende obtener datos desde aplicación, infectar el sistema operativo, acceder a contraseñas, etc.
6.    La contraseña y los datos necesarios para la autenticación de los usuarios en los sistemas debe estar cifrada en los medios de almacenamiento. El estándar de cifrado para las contraseñas es el MD5.

1. LINEAMIENTOS PARA EL MANEJO DEL DESARROLLO
- La aplicación o sistema debe ser construido siguiendo un desarrollo en N-Capas, separando claramente como mínimo la capa de presentación, la lógica de negocio y el acceso a datos. El objetivo del diseño es que las capas no queden acopladas fuertemente para que la solución sea flexible a los cambios. Cada capa debe tener claramente delimitada su responsabilidad, de igual forma cada capa únicamente se comunica con su capa inferior o su capa superior, no debe existir comunicación entre capas que no sean inmediatas ya sea por su nivel inferior o superior.
- Se debe usar el Modelo Orientado a Objetos.
- Los métodos y procedimientos desarrollados deben tener documentación en la que se especifique la funcionalidad, las entradas y las salidas, para esto utilizar el estándar de comentarios proporcionado por Visual Studio, documentando la responsabilidad de cada clase, el uso de cada parámetro, método y evento. Ejemplo:
- Los métodos y procedimientos desarrollados deben tener documentación en la que se especifique la funcionalidad, las entradas y las salidas, para esto utilizar el estándar de comentarios proporcionado por Visual Studio, documentando la responsabilidad de cada clase, el uso de cada parámetro, método y evento.
- Se debe utilizar Windows Communication Foundation (WCF) para la creación de servicios.
- Uso de caché para disminuir al máximo la llamada al servidor si se construyen aplicaciones web.
- Se debe validar las entradas desde la capa de presentación.
- Uso de ayudas para agilizar la digitación en las interfaces de captura.
- Uso de tooltips.
- Manejo ágil de listas y combos para la selección de opciones con una cantidad no mayor a 50 ítems o utilización de filtrado inicial para resultados de más de 50 ítems.
- En caso de hacerse necesario el uso de lenguajes Script jscript, deben hacerse llamados a funciones contenidas en archivos Script.

1.1.1    Estructuras (namespaces, procedimientos, clases, interfaces y propiedades)
- Los nombres de todas las estructuras de código deben ser en español.
- Los namespaces deben empezar por el nombre de la entidad  seguido de la unidad de negocio, el producto o proyecto y la funcionalidad ejemplo:


"Kestrel": {
    "EndPoints": {
      "Http": {
        "Url": "http://+:5001"
      },
    "Https": {
        "Url": "https://+:5002"
      }
    }
  }
