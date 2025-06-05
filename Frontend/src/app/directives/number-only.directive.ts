import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[appNumberOnly]'
})
export class NumberOnlyDirective {
  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent): void {
    const allowedKeys = ['Backspace', 'Tab', 'ArrowLeft', 'ArrowRight', 'Delete', 'Enter'];
    const isNumberKey = event.key >= '0' && event.key <= '9';
    const isModifierKey = event.ctrlKey || event.metaKey || event.altKey;

    if (!isNumberKey && !allowedKeys.includes(event.key) && !isModifierKey) {
      event.preventDefault();
    }
  }

  @HostListener('paste', ['$event'])
  onPaste(event: ClipboardEvent): void {
    const clipboardData = event.clipboardData?.getData('text') || '';
    if (!/^\d+$/.test(clipboardData)) {
      event.preventDefault();
    }
  }
}
