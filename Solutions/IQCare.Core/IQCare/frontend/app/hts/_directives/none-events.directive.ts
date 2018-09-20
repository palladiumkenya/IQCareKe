import {Directive, ElementRef, Renderer} from '@angular/core';

@Directive({
  selector: '[appNoneEvents]'
})
export class NoneEventsDirective {

  constructor(private el: ElementRef, private renderer: Renderer) {
    renderer.setElementStyle(el.nativeElement, 'pointerEvents', 'none');
  }

}
