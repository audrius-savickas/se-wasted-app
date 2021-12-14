import {useDispatch, useSelector} from "react-redux"
import {setCustomerId} from "../actions/customer"
import {getCustomerId} from "../selectors/user"

export const useCustomer = () => {
  const dispatch = useDispatch()

  return {
    customerId: useSelector(getCustomerId),
    setCustomerId: ({customerId}: {customerId: string}) => dispatch(setCustomerId({customerId}))
  }
}
