import moment from "moment"
import React, {useEffect, useState} from "react"
import {Alert, StyleSheet} from "react-native"
import {BorderRadiuses, Button, Colors, Text} from "react-native-ui-lib"
import {cancelFoodReservation, reserveFood} from "../../../api/reservation"
import {useCustomer} from "../../../hooks/use-customer"
import {formatPrice} from "../../../utils/currency"
import {formatDate, formatTime} from "../../../utils/date"
import {ReservationInfoProps} from "./interfaces"

export const CustomerReservationInfo = ({food}: ReservationInfoProps) => {
  const {customerId} = useCustomer()

  const [timeLeft, setTimeLeft] = useState(0)
  const [reservation, setReservation] = useState(food.reservation)

  const {id, currentPrice} = food

  const makeReservation = async () => {
    const response = await reserveFood({foodId: id, customerId})
    if (response) {
      console.log("reservation made")
      setReservation({reservedAt: new Date().toString(), foodId: food.id, customerId, price: currentPrice})
    } else {
      console.log("reservation failed")
    }
  }

  const cancelReservation = async () => {
    const response = await cancelFoodReservation({foodId: id, customerId})
    if (response) {
      console.log("reservation cancelled")
      setReservation(null)
    } else {
      console.log("reservation cancellation failed")
    }
  }

  const makeReservationAlert = () => {
    Alert.alert(
      "Make reservation?",
      "If you don't pick it up in 30 minutes, you will receive penalty points to your account.",
      [{text: "OK", onPress: makeReservation}, {text: "Cancel"}]
    )
  }

  const cancelReservationAlert = () => {
    Alert.alert(
      "Cancel reservation?",
      "If you cancel this reservation, you won't be able to pick up this food unless reserved again.",
      [{text: "OK", onPress: cancelReservation}, {text: "Cancel"}]
    )
  }

  useEffect(() => {
    if (reservation) {
      setTimeLeft(Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60))
      const interval = setInterval(() => {
        const newMinutes = Math.round(moment(reservation.reservedAt).add(30, "minutes").diff(moment()) / 1000 / 60)
        if (newMinutes > 0) {
          setTimeLeft(newMinutes)
        }
      }, 60000)

      return () => clearInterval(interval)
    }
  }, [reservation])

  return reservation ? (
    <>
      <Text left text70L>
        You have reserved this food for <Text text70M>{formatPrice(reservation.price)}</Text> at:
      </Text>
      <Text center text70M style={styles.greyed}>
        {formatDate(reservation.reservedAt.toString())}, {formatTime(reservation.reservedAt.toString())}.
      </Text>
      <Text left marginT-s3 text70L>
        Time left to pick up your reservation:
      </Text>
      <Text center text70M style={styles.greyed}>
        {timeLeft} minutes.
      </Text>
      <Text left marginT-s3 text70L>
        You may cancel your reservation at any time with the button below.
      </Text>
      <Button marginV-s3 label="Cancel Reservation" style={styles.button} onPress={cancelReservationAlert} />
    </>
  ) : (
    <>
      <Text left text70L>
        This food is not reserved yet. Press the button below to reserve it for 30 minutes.
      </Text>
      <Button marginV-s3 label="Reserve" style={styles.button} onPress={makeReservationAlert} />
    </>
  )
}

const styles = StyleSheet.create({
  button: {
    alignSelf: "center"
  },
  greyed: {
    borderRadius: BorderRadiuses.br30,
    backgroundColor: Colors.grey60,
    borderColor: Colors.grey50,
    borderWidth: 1
  }
})
