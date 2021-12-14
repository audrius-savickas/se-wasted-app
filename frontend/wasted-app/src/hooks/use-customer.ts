import {useDispatch, useSelector} from "react-redux"
import {logOutCustomer, setCustomerId} from "../actions/customer"
import {getCustomerId} from "../selectors/customer"

export const useCustomer = () => {
  const dispatch = useDispatch()

  return {
    customerId: useSelector(getCustomerId),
    setCustomerId: ({customerId}: {customerId: string}) => dispatch(setCustomerId({customerId})),
    logOutCustomer: () => dispatch(logOutCustomer())
  }
}
