import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/models/User';
import { UserService } from 'src/services/user.service';
import {animate, state, style, transition, trigger} from '@angular/animations';


@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class UsersListComponent implements OnInit {

  private id: number;
  public dataSource = new MatTableDataSource<User>();
  user: User | null;
  public displayedColumns: string[] = ['name', 'lastName', 'email', 'country', 'weightAverage', 'heartRateAverage', 'bilurubinAverage' ,'lastanalizDate', 'totalDebt', 'totalCredit'];
  public selection = new SelectionModel<User>(true, []);
  expandedElement: User | null; 

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private snackbar: MatSnackBar,
              public createDialog: MatDialog
  ) { }

  ngOnInit(): void {
    const id = 'id';
    this.route.params.subscribe(params => this.id = params[id]);
    this.userService.fetchUsers().subscribe(x => this.dataSource = new MatTableDataSource(x));
  }
}

