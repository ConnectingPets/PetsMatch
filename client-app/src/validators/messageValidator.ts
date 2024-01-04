import { combineValidators, composeValidators, hasLengthLessThan, isRequired } from "revalidate";

export const sendMessageValidator = combineValidators({
  Content: composeValidators(
    isRequired,
    hasLengthLessThan(351)
  )('Content')
});