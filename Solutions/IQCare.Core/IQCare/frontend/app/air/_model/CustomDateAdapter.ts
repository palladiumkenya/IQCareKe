import { Component, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NativeDateAdapter, DateAdapter, MatDatepicker } from '@angular/material';
import * as _moment from 'moment';


const moment =  _moment;

export class CustomDateAdapter extends NativeDateAdapter {
  format(date: Date, displayFormat: Object): string {
    let formatString = 'MMMM YYYY';
    return moment(date).format(formatString);
  }
}