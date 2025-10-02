# 🏗️ Knight Adventure: Архитектурное демо

<img alt="Version" src="https://img.shields.io/badge/Unity-2021.3.45f1-000.svg"/> <img alt="Version" src="https://img.shields.io/badge/Version-0.3.3-blue.svg?cacheSeconds=2592000" />

**Технический демо-проект**, созданный для изучения и демонстрации построения чистой b масштабируемой архитектуры в Unity.  
Основная цель - не геймплей, а качество кода и архитектурные решения.

> [!WARNING] 
> Этот проект был разработан в рамках курса от K-Syndicate `Архитектура мобильных игр`.  
> Весь геймплей служит контекстом для демонстрации подходов к архитектруе игр.

## 🎥 Демонстрация работы

<a href="https://ingolf.itch.io/knight-adventure-architecture-demo" target="_blank">
  <img src="https://img.shields.io/badge/Сыграть в WebGL Build на itch.io-FA5C5C?style=for-the-badge&logo=itchdotio&logoColor=white" alt="Demo_on_itchdotio"/>  
</a>

### 🎬 Скриншоты

<div align="center">
  <img src="https://github.com/user-attachments/assets/ebab3214-824e-481b-9953-df5ca5ff9edc" align="center" />
  <p>Бой с противниками, нанесение урона противникам и игроку</p>

  <img src="https://github.com/user-attachments/assets/91001eee-e664-44f4-9de9-c6666d01f000" align="center" />
  <p>Лут выдающий с противников можно поднять и получить валюту - черепа</p>

  <img src="https://github.com/user-attachments/assets/71e65f46-b1eb-4cec-8ad5-4168c4a6ab87" align="center" />
  <p>По кнопке "Shop" на экране открывается магазин, в котором можно получить черепа за просмотр рекламы или за донат</p>
</div>

## 🎮 Реализованные игровые фичи

✅ Перемещение и атака игрока  
✅ Система здоровья и смерть сущностей  
✅ Лут со сбором валюты в виде черепов  
✅ Сохранение и загрузка состояния ирока и игровго мира (позиции, прогресс, лут)  
✅ Система переходов на другие уровни  
✅ HUD с отображением здоровья и валюты  
✅ Магазин с интеграцией рекламы с вознаграждением и внутриигровых покупок  
✅ Точки сохранения  

## 🧩 Архитектура проекта

### 🏗️ Обзор Архитектуры
- **GameBootstrapper**: Единая точка входа в программу.
- **State Machine**: Управляет потоком игры - начальная загрузка, загрузка прогресса, загрузка уровня.
- **Внедрение зависимостей**: Кастомный DI-контейнер (в основе паттерн Service Locator) - используется для регистрации и разрешения зависимостей.
- **Сервисы**: Каждая система разделена на сервисы (`AdsService`, `SaveLoadService`), которые регистрируются в DI-контейнере.
- **Слои данных**:
  - Статические данные: ScriptableObjects для конфигурации (статы врагов, статы игрока).
  - Динамические данные: JSON-сериализованные классы, хранящиеся в PlayerPrefs для прогресса игрока.
- **Паттерн Factory**: `GameFactory` отвечает за создание сущностей (игрок, враги, лут) с использованием префабов, загружаемых через сервис `IAssetProvider`.

### 🔧 Ключевые сервисы
| Сервис | Назначение |
|--------|------------|
| `AssetProvider` | Загрузка префабов через папку `Resources` |
| `GameFactory` | Создание сущностей через паттерн Factory |
| `PersistentProgressService` | Управление прогрессом игрока |
| `SaveLoadService` | Сохранение/загрузка данных |
| `StaticDataService` | Доступ к конфигурационным данным |
| `InputService` | Абстракция системы ввода |
| `AdsService` | Интеграция Unity Ads |
| `IAPService` | Сервис внутриигровых покупок |

## 🛠 Технологии и паттерны

**Основные технологии:**
`C#` • `Unity` • `Unity Ads` • `Unity IAP` • `Git`  

**Архитектурные принципы:**
`SOLID` • `OOP` • `Dependency Injection` • `Service Locator`  

**Паттерны проектирования:**
`State` • `Factory` • `Service` • `Data Layer`  

## ⚙️ Установка и запуск
1. **Клонируйте репозиторий (или скачайте проект)**:
   ```bash
   git clone https://github.com/ingolfTHEcapitan/Mobile-Game-Architecture.git
2. **Откройте проект в Unity версии 2021.3.45f1**
3. **Откройте сцену LevelOne: "Assets\ _Game\Scenes\LevelOne.unity"**
4. **нажмите ▶ Play, чтобы сразу попасть в игру.**

Альтернативно вы можите сыграть в WebGL билд на  [![itchdotio](https://img.shields.io/badge/itch.io-FA5C5C.svg?&style=flat&logo=itchdotio&logoColor=white)](https://ingolf.itch.io/knight-adventure-architecture-demo)


## 🔄 Сброс сохранённого прогресса
В меню Unity Editor перейди в Tools → ClearPlayerPrefs — прогресс и данные будут сброшены.

