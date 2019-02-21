import { Pipe, PipeTransform } from '@angular/core';
import {SubSection,Section} from  '../Sectionidentifier';
import {IndicatorQuestionBase} from '../indicatorquestion-base'

@Pipe({
    name: 'subSectionfilter',
   
})
export class SubSectionFilterPipe implements PipeTransform {
    transform(items: SubSection[], filter: number): any {
        if (!items || !filter) {
            return items;
        }
        // filter items array, items which match and return true will be
        // kept, false will be filtered out
        return items.filter(item => item.SectionId=== filter);
    }
}


@Pipe({
    name: 'IndicatorFilter',
  
})
export class IndicatorFilterPipe implements PipeTransform {
    transform(items: IndicatorQuestionBase[], filter: number): any {
        if (!items || !filter) {
            return items;
        }
        // filter items array, items which match and return true will be
        // kept, false will be filtered out
        return items.filter(item => item.SubSectionId === filter);
    }
}