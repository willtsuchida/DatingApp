import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {//tirar dos parametros  currentRoute, currentState, nextState
  //passamos na Fn o component
  //usando o parametro component temos acesso as variaveis dele
  if (component.editForm?.dirty){
    return confirm('Are you sure you want to continue? Any unsaved changes will be lost')
  }
  return true;
};
