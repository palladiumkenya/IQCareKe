import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatconfirmdialogComponent} from '../matconfirmdialog/matconfirmdialog.component';
@Injectable({
    providedIn: 'root'
})
export class DialogService {

    constructor(private dialog: MatDialog) { }

    openConfirmDialog(msg) {
        return this.dialog.open(MatconfirmdialogComponent, {
            width: '390px',
            panelClass: 'confirm-dialog-container',
            disableClose: true,
            position: { top: '10px' },
            data: {
                message: msg
            }
        });
    }
}
