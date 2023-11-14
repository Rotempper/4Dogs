import { Component } from '@angular/core';
import { User } from './model/userModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Four-Dogs';
  currentUser!: User;

  constructor(){}
}
