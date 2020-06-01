import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ScoringSystemAngular';
  userId: number;

  constructor(private router: Router){

  }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      this.userId = payLoad.userId;
    }
  }

  onLogOut(){
    localStorage.removeItem('token');
    this.router.navigate(['/main']);
  }
}
