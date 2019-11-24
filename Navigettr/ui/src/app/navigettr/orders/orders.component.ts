import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  // imageChangedEvent: any = '';
  pagesIndex: Array<number>;
  pageStart: number = 1;
  pages: number = 4;
  email = "";

  Element: any = [
    {  firstName: 'Mike', lastName: 'Hussey', email: 'mike@gmail.com' },
    {  firstName: 'Ricky', lastName: 'Hans', email: 'ricky@gmail.com' },
    {  firstName: 'Martin', lastName: 'Kos', email: 'martin@gmail.com' },
    {  firstName: 'Tom', lastName: 'Paisa', email: 'tom@gmail.com' },
    {  firstName: 'John', lastName: 'Doe', email: 'john@gmail.com' },
    {  firstName: 'Mike', lastName: 'Hussey', email: 'mike@gmail.com' },
    {  firstName: 'Ricky', lastName: 'Hans', email: 'ricky@gmail.com' },
    {  firstName: 'Martin', lastName: 'Kos', email: 'martin@gmail.com' },
    {  firstName: 'Tom', lastName: 'Paisa', email: 'tom@gmail.com' },
    {  firstName: 'John', lastName: 'Doe', email: 'john@gmail.com' },
    {  firstName: 'Mike', lastName: 'Hussey', email: 'mike@gmail.com' },
    {  firstName: 'Ricky', lastName: 'Hans', email: 'ricky@gmail.com' },
    {  firstName: 'Martin', lastName: 'Kos', email: 'martin@gmail.com' },
    {  firstName: 'Tom', lastName: 'Paisa', email: 'tom@gmail.com' },
    {  firstName: 'John', lastName: 'Doe', email: 'john@gmail.com' },
    {  firstName: 'Mike', lastName: 'Hussey', email: 'mike@gmail.com' },
    {  firstName: 'Ricky', lastName: 'Hans', email: 'ricky@gmail.com' },
    {  firstName: 'Martin', lastName: 'Kos', email: 'martin@gmail.com' },
    {  firstName: 'Tom', lastName: 'Paisa', email: 'tom@gmail.com' },
    {  firstName: 'John', lastName: 'Doe', email: 'john@gmail.com' },
    {  firstName: 'Mike', lastName: 'Hussey', email: 'mike@gmail.com' },
    {  firstName: 'Ricky', lastName: 'Hans', email: 'ricky@gmail.com' },
    {  firstName: 'Martin', lastName: 'Kos', email: 'martin@gmail.com' },
    {  firstName: 'Tom', lastName: 'Paisa', email: 'tom@gmail.com' }
  ];


  constructor() { }

  ngOnInit() {
  }

}
