import moment from "moment"
import React, {useEffect, useState} from "react"
import {Checkbox, DateTimePicker, Slider, Stepper, View, Text} from "react-native-ui-lib"
import {DecreaseType} from "../../../../api/interfaces"
import {Props} from "./interfaces"

interface IPriceDecreasing {
  startDecreasingAt: string
  minimumPrice: number
  intervalTimeInMinutes: number
  amountPerInterval: number
  percentPerInterval: number
  decreaseType: DecreaseType
}

export const PriceDecreasing = ({food, setFood}: Props) => {
  const [time, setTime] = useState<Date>(new Date())
  const [date, setDate] = useState<Date>(new Date())
  const [decreaseType, setDecreaseType] = useState<DecreaseType>(food.decreaseType)

  const [priceDecreasing, setPriceDecreasing] = useState<IPriceDecreasing>({
    startDecreasingAt: food.startDecreasingAt,
    minimumPrice: food.minimumPrice,
    intervalTimeInMinutes: food.intervalTimeInMinutes,
    amountPerInterval: food.amountPerInterval,
    percentPerInterval: food.percentPerInterval,
    decreaseType: food.decreaseType
  })

  const onChangeMinimumPrice = (minimumPrice: number) => {
    setPriceDecreasing({...priceDecreasing, minimumPrice})
    setFood({...food, minimumPrice})
  }

  const onChangeInterval = (intervalTimeInMinutes: number) => {
    setPriceDecreasing({...priceDecreasing, intervalTimeInMinutes})
    setFood({...food, intervalTimeInMinutes})
  }

  const onChangeAmountPerInterval = (amountPerInterval: number) => {
    setPriceDecreasing({...priceDecreasing, amountPerInterval})
    setFood({...food, amountPerInterval})
  }

  const onChangePercentPerInterval = (percentPerInterval: number) => {
    setPriceDecreasing({...priceDecreasing, percentPerInterval: percentPerInterval * 100})
    setFood({...food, percentPerInterval: percentPerInterval * 100})
  }

  const onChangeDecreaseType = (x: DecreaseType) => {
    setDecreaseType(x)
    setFood({...food, decreaseType: x})
  }

  useEffect(() => {
    const newDate = new Date()
    newDate.setFullYear(date.getFullYear())
    newDate.setMonth(date.getMonth())
    newDate.setDate(date.getDate())
    newDate.setHours(time.getHours())
    newDate.setMinutes(time.getMinutes())

    const momentDate = moment(newDate)

    setPriceDecreasing({...priceDecreasing, startDecreasingAt: momentDate.toISOString()})
    setFood({...food, startDecreasingAt: momentDate.toISOString()})
  }, [time, date])

  return (
    <View flex centerV marginH-s6>
      <Text marginB-s2>Start decreasing at</Text>
      <View center>
        <View row centerV>
          <DateTimePicker style={{width: 170}} title="Date" mode="date" value={date} onChange={setDate} />
          <DateTimePicker style={{width: 170}} title="Time" mode="time" value={time} onChange={setTime} />
        </View>
      </View>
      <View marginT-s4 marginB-s4>
        <Text>Minimum price: {food.minimumPrice.toFixed(2)}</Text>
        <Slider
          minimumValue={0}
          maximumValue={food.currentPrice}
          value={0}
          step={0.01}
          onValueChange={onChangeMinimumPrice}
        />
      </View>
      <View marginT-s8 marginB-s4>
        <Text marginB-s1>Decrease price interval (mins)</Text>
        <Stepper
          small
          minValue={0}
          maxValue={180}
          step={5}
          value={food.intervalTimeInMinutes}
          onValueChange={onChangeInterval}
        />
      </View>

      <View marginT-s8>
        <View marginB-s4>
          <Checkbox
            value={decreaseType === DecreaseType.AMOUNT}
            label="Amount"
            onValueChange={() => {
              onChangeDecreaseType(DecreaseType.AMOUNT)
            }}
          />
          <Checkbox
            marginT-s1
            value={decreaseType === DecreaseType.PERCENT}
            label="Percent"
            onValueChange={() => {
              onChangeDecreaseType(DecreaseType.PERCENT)
            }}
          />
        </View>

        <View marginT-s8>
          <Text>
            Decrease step:{" "}
            {decreaseType === DecreaseType.AMOUNT
              ? food.amountPerInterval.toFixed(2) + "€"
              : food.percentPerInterval.toFixed(0) + "%"}
          </Text>
          <Slider
            minimumValue={0}
            maximumValue={decreaseType === DecreaseType.AMOUNT ? 10 : 1}
            value={0}
            step={0.01}
            onValueChange={x => {
              decreaseType === DecreaseType.AMOUNT ? onChangeAmountPerInterval(x) : onChangePercentPerInterval(x)
            }}
          />
        </View>
      </View>
    </View>
  )
}
