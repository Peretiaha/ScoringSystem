import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/services/user.service';
import { User } from 'src/models/User';
import { UsersHealth } from 'src/models/UsersHealth';
import { PageEvent } from '@angular/material/paginator/paginator';
import { BankAccount } from 'src/models/BankAccount';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  active = 1;
  user = new User();
  pageEventHealth: PageEvent;
  pageEventBankAccount: PageEvent;
  userHealth = new Array<UsersHealth>();
  userBankAccounts = new Array<BankAccount>();

  constructor(
    private userService: UserService,
    public translate: TranslateService
  ) { }

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe(x=> {
      this.user = x;  
      this.userBankAccounts = x.bankAccounts;
      this.userHealth = x.usersHealth.sort((a, b) => new Date(b.health.analizDate).getTime() - new Date(a.health.analizDate).getTime());
    });
  }
}
