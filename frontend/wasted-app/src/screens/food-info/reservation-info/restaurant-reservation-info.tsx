import moment from "moment"
import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {BorderRadiuses, Colors, Text} from "react-native-ui-lib"
import {getCustomerById} from "../../../api/customer"
import {Customer} from "../../../api/interfaces"
import {formatDate, formatTime} from "../../../utils/date"
import {ReservationInfoProps} from "./interfaces"

export const RestaurantReservationInfo = ({food}: ReservationInfoProps) => {
  const {reservation} = food

  const [timeLeft, setTimeLeft] = useState(0)
  const [customer, setCustomer] = useState({} as Customer)

  const fetchCustomer = async () => {
    if (reservation) {
      const response = await getCustomerById({customerId: reservation?.customerId})
      if (response) {
        setCustomer(response)
      } else {
        console.error("fetch customer failed")
      }
    }
  }

  useEffect(() => {
    if (reservation) {
      fetchCustomer()
      setTimeLeft(Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
      const interval = setInterval(() => {
        setTimeLeft(Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
      }, 60000)

      return () => clearInterval(interval)
    }
  }, [reservation])

  return reservation ? (
    <>
      <Text left text70L>
        Customer {`${customer.firstName} ${customer.lastName}`} has reserved this meal on
      </Text>
      <Text center text70M style={styles.greyed}>
        {formatDate(reservation.reservedAt.toString())}, {formatTime(reservation.reservedAt.toString())}.
      </Text>
      <Text left marginT-s2 text70L>
        Time left for the customer to pick up their reservation:
      </Text>
      <Text center text70M style={styles.greyed}>
        {timeLeft} minutes.
      </Text>
    </>
  ) : (
    <>
      <Text left text70L>
        This food is not reserved yet. Please wait for customers to reserve it!
      </Text>
    </>
  )
}

const styles = StyleSheet.create({
  greyed: {
    borderRadius: BorderRadiuses.br30,
    backgroundColor: Colors.grey60,
    borderColor: Colors.grey50,
    borderWidth: 1
  }
})
