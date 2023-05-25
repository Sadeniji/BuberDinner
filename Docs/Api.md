# Buber Dinner API

Table of Contents
- [Buber Dinner API](#buber-dinner-api)
    - [Auth](#auth)
        - [Register](#register)
            - [Register Request](#register-request)
            - [Register Response](#register-response)
        - [Login](#login)
            - [Login Request](#login-request)
            - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

### Register Request

```json
{
    "firstName": "Amichai",
    "lastName": "Mantinband",
    "email": "amichai@mantinband.com",
    "password": "Amiko1231!"
}
```

### Register Response

```json
200 OK
```

```json
{
    "id": 
    "firstName": "Amichai",
    "lastName": "Mantinband",
    "email": "amichai@mantinband.com",
    "token": "eyJhb.z9dqcXoy"
}
```
### Login

```js
POST {{host}}/auth/login
```

### Login Request

```json
{
    "email": "amichai@mantinband.com",
    "password": "Amiko1231!"
}
```

### Login Response

```json
200 OK
```