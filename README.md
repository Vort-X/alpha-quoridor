# alpha-quoridor
Наша команда 'alpha' займається розробкою версії для персональних комп'ютерів настільної гри 'Quoridor'.
Гра написана на мові програмування C# з використанням ігрового двигуна Godot.

Рішення складається з трьох проектів:
1. Queridor - містить модель дошки, логіку її створення, сервіси валідації та виконання ходів, алгоритм пошуку шляху. 
2. Quoridor.Model - обгортка над Queridor, містить гравців, модель представлення дошки, інкапсуляцію та логіку створення ходів.
3. UI.Godot - користувацький інтерфейс на Godot, використовує Quoridor.Model для створення гри, містить відображення та контроллери, ігрові меню та спрайти об'єктів.

Наразі проект має логіку створення нового об'єкту гри, готову модель та логіку використання дошки, формування, перевірку та виконання ходів, алгоритм пошуку шляху (A\*).
З боку користувацького інтерфейсу маємо ігрове меню з можливістю переходу між відображеннями, спрайти дошки, пішок, стін, підсвічування розположення стін.

# Скріншоти гри:
![Головне меню](https://lh3.googleusercontent.com/9pfN3OLkHQP1saf0Ytb2Dm83eZMAoBkaHDpPx0R2EDCVqMwLC-3kZCnQNSJrUugMVAThBuUJy3HcRlux5eWV=w1920-h969-rw)
![Вибір гри](https://lh6.googleusercontent.com/nSyAfuMr8BVUc0kB7DTQCZmdWY4nJATqyj8fqsa0Lo1__kVKwpl_uQI7olfczfmzCssEnKXgZ8rFaWhcdwlE=w1920-h969-rw)
![Дошка з пішками](https://lh5.googleusercontent.com/jjMXEZlxqd-fncUiX172X3vqCJSI5sGopfimWuPSIUtN6hxXuOMTpFiC2SCtfP8BF1ex5MaQxmHEG6krUq9m=w1920-h969-rw)
![Розміщення стіни](https://lh5.googleusercontent.com/mSb8eOb8s-WJknij5VFl8qKQJTuy7bbdweTFi8JJwv9k28QPSZywnpmWF-m29kxlzELr7kI0gyPsjeuWzBvq=w1920-h969-rw)
![Підсвітка клтинок при розміщенні](https://lh5.googleusercontent.com/NdhC4Zz2oZSbWSA-6wYwQ5e8c2_CBV59RPmL3rRdr1Fnrm4_0v_-nVSkRsqkGXtJ0TyHOASSg6mzWOD_JXD0=w1920-h969-rw)
