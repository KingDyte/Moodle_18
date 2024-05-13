# Rendszerfejlesztés Moodle feladat, 18-as csapat

- Milei Örs (PUM5UW)

# REQUIREMENTS

- php - min 8.2
- composer - min 2.6
- XAMPP or LAMP
- Git

# INSTALL

- Szükséges könyvtárak

```shell
composer install
```

- NPM csomagok

```shell
npm install
```

- Konfigurációk

```shell
cp .env.example .env
```

- Adatbázis beállítása az .env fájlban DB_DATABASE=??? stb

- Adatbázis migrálása

```shell
php artisan migrate
```

- Példa adatok

```shell
php artisan db:seed
```

- Virtual host regisztrálása a httpd-vhosts.conf-ba (hosts beállítása) -[PATH] ki kell cserelni az eleresi utra, ahol a feladat van

```apacheconf
<VirtualHost *:80>
    DocumentRoot "[PATH]\public"
    ServerName feladat.devel
    ErrorLog "logs/feladat-error.log"
    CustomLog "logs/feladat.log" common
</VirtualHost>
```

# START

- NPM dev környezet indítása

```shell
npm run dev
```

- Websocket (új shellben)

```shell
php artisan websockets:serve
```

Elso indulasnal generalni kell egy APP key-t, ra kell menni a generate key-re

- Beállított virtual host megtekintése böngészőben
- Teszt felhasználók:

  - Email: *diak1@example.com*
  - Password: _12345_
  - Email: *diak2@example.com*
  - Password: _12345_

- Websocket tesztelés a következő címen: http://feladat.devel/api/wstest
