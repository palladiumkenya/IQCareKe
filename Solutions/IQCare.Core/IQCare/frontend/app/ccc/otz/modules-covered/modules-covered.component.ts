import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {LookupItemView} from '../../../shared/_models/LookupItemView';
import {OtzService} from '../../_services/otz.service';

@Component({
  selector: 'app-modules-covered',
  templateUrl: './modules-covered.component.html',
  styleUrls: ['./modules-covered.component.css']
})
export class ModulesCoveredComponent implements OnInit {
    title: string;
    form: FormGroup;
    topics: LookupItemView[];
    
    constructor(private fb: FormBuilder,
                private dialogRef: MatDialogRef<ModulesCoveredComponent>,
                @Inject(MAT_DIALOG_DATA) data,
                private otzService: OtzService) {
        this.title = 'Topics Covered';
    }
    
    async ngOnInit() {
        this.form = this.fb.group({
            topic: new FormControl('', [Validators.required]),
            dateCompleted: new FormControl('', [Validators.required]),
        });
        
        const otzModules = await this.otzService.getByGroupName('OTZ_Modules').toPromise();
        this.topics = otzModules['lookupItems'];
    }

    Close() {
        this.dialogRef.close();
    }

    Save() {
        if (this.form.valid) {
            this.dialogRef.close(this.form.value);
        } else {
            return;
        }
    }
}
