import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {  MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule} from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule  } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import {MatRadioModule} from '@angular/material/radio';
import {MatOptionModule} from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';

@NgModule({
    exports: [
        MatCheckboxModule,
        MatInputModule,
        MatRadioModule,
        MatOptionModule,
        MatSelectModule,
        MatSnackBarModule,
        MatDialogModule,
        MatButtonModule,
        MatSnackBarModule,
        MatTableModule,
        MatDialogModule,
        MatIconModule,
        MatToolbarModule,
    ]
  })

export class MaterialModule {}