import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  userId: number;

  constructor() { }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      this.userId = payLoad.userId;
    }
  }

}
